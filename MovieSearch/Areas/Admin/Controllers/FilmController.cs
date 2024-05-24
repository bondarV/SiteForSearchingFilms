using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using MovieSearch.Areas.Admin.Controllers.Dto;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;
using MovieSearch.Utility;

namespace MovieSearch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class FilmController : Controller
    {
        private const string BASE_URL = "https://api.themoviedb.org/3/discover/movie/";
        private const string API_KEY = "cef4b41f48bbd43460688be4a6c28f23";

        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;

        public FilmController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<List<FilmDto>> GetFilms(int pageNumber)
        {
            var requestUrl = $"{BASE_URL}?api_key={API_KEY}&page={pageNumber}";
            var films = new List<FilmDto>();

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var filmList = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(filmList))
                {
                    var filmData = JsonConvert.DeserializeObject<dynamic>(filmList);
                    var filmsArray = filmData.results;
                    films = JsonConvert.DeserializeObject<List<FilmDto>>(filmsArray.ToString());
                }
            }

            return films;
        }

        public async Task<IActionResult> Index()
        {
            for (int page = 1; page <= 10; page++)
            {
                var filmDtos = await GetFilms(page);
                foreach (var filmDto in filmDtos)
                {
                    var film = new Film
                    {
                        Title = filmDto.Title,
                        Overview = filmDto.Overview,
                        Id = filmDto.Id,
                        Adult = filmDto.Adult,
                        Backdrop_Path = filmDto.Backdrop_Path,
                        Original_Title = filmDto.Original_Title,
                        Original_Language = filmDto.Original_Language,
                        Popularity = filmDto.Popularity,
                        Poster_Path = filmDto.Poster_Path,
                        Vote_Average = filmDto.Vote_Average,
                        Vote_Count = filmDto.Vote_Count,
                        Release_Date = filmDto.Release_Date
                    };

                    _unitOfWork.Film.Add(film);
                    _unitOfWork.Film.Update(film);

                    foreach (var genreId in  filmDto.Genre_Ids)
                    {
                        var filmGenre = new FilmGenre
                        {
                            FilmId = film.Id,
                            GenreId = genreId
                        };

                        _unitOfWork.FilmGenre.Add(filmGenre);
                        _unitOfWork.FilmGenre.Update(filmGenre);
                    }
                }

                _unitOfWork.Save();
            }

            var filmsList = _unitOfWork.Film.GetAll().Take(3);
            return View(filmsList);
        }
    }
}
