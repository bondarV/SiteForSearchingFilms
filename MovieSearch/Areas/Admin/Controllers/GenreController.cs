using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;
using MovieSearch.Utility;

namespace MovieSearch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GenreController : Controller
    {
        string BASE_URL = "https://api.themoviedb.org/3/genre/movie/list";
        string API_KEY = "cef4b41f48bbd43460688be4a6c28f23";
        HttpClient httpClient;

        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<List<Genre>> GetGenres()
        {
            string requestUrl = $"{BASE_URL}?api_key={API_KEY}";
            string genreList = "";
            List<Genre>? genres = null;

            httpClient.BaseAddress = new Uri(requestUrl);
            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                genreList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (!genreList.Equals(""))
            {
                // Deserialize the JSON content into a dynamic object
                var genreData = JsonConvert.DeserializeObject<dynamic>(genreList);

                // Extract the genres array (assuming it's nested under a "genres" key)
                var genresArray = genreData.genres;

                // Now, convert the genresArray to a list of Genre objects
                genres = JsonConvert.DeserializeObject<List<Genre>>(genresArray.ToString());
            }

            return genres;

        }

        public async Task<IActionResult> Index()
        {
            List<Genre> genres = await GetGenres();
                foreach (var genre in genres)
                {
                    var existingGenre = _unitOfWork.Genre.Get(g => g.Id == genre.Id);

                    if (existingGenre == null)
                    {
                        _unitOfWork.Genre.Add(genre);
                        _unitOfWork.Genre.Update(genre);
                    }
                }
            _unitOfWork.Save();

                var data = _unitOfWork.Genre.GetAll();
                
            return View(data); 
        }
    }
}
