using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model.Models;

namespace MovieSearch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FilmController : Controller
    {
        string BASE_URL = "https://api.themoviedb.org/3/trending/movie/week";
        string API_KEY = "cef4b41f48bbd43460688be4a6c28f23";

        HttpClient httpClient;
        private readonly IUnitOfWork _unitOfWork;

        public FilmController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<List<Film>> GetFilms()
        {
            string requestUrl = $"{BASE_URL}?api_key={API_KEY}";
            string filmList = "";
            List<Film> films = new List<Film>(); // Use List<Film>

            httpClient.BaseAddress = new Uri(requestUrl);
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                filmList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (!filmList.Equals(""))
            {
                // Deserialize the JSON content into a dynamic object
                var filmData = JsonConvert.DeserializeObject<dynamic>(filmList);

                // Extract the films array (assuming it's nested under "results")
                var filmsArray = filmData.results;

                // Now, convert the filmsArray to a list of Film objects
                films = JsonConvert.DeserializeObject<List<Film>>(filmsArray.ToString());
            }

            return films;
        }

        public async Task<IActionResult> Index()
        {
            var films = await GetFilms();
            foreach (var film in films)
            {
                // Check if the film already exists in the database
                var existingFilm = _unitOfWork.Film.Get(f => f.Id == film.Id);

                if (existingFilm == null)
                {
                    // Add the new film to the database
                    _unitOfWork.Film.Add(film);
                    foreach (var genreId in film.Genre_Ids)
                    {
                        var filmGenre = new FilmGenre
                        {
                            FilmId = film.Id,
                            GenreId = genreId
                        };
                        _unitOfWork.FilmGenre.Add(filmGenre);
                    }
                }
            }
            _unitOfWork.Save();
            var filmsList = _unitOfWork.Film.GetAll();
            return View(filmsList);
        }
    }
}