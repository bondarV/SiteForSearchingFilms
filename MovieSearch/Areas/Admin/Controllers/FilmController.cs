using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using MovieSearch.Areas.Admin.Controllers.Dto;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;

namespace MovieSearch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FilmController : Controller
    {
        string BASE_URL = "https://api.themoviedb.org/3/discover/movie";
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

        private async Task<List<FilmDto>> GetFilms(int page)
        {
            string requestUrl = $"{BASE_URL}?api_key={API_KEY}&page={page}&with_release_type&sort_by=vote_count.desc";  
            string filmList = "";
            var films = new List<FilmDto>(); // Use List<FilmDto>

            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                filmList = await response.Content.ReadAsStringAsync();
            }

            if (!string.IsNullOrEmpty(filmList))
            {
                // Deserialize the JSON content into a dynamic object
                var filmData = JsonConvert.DeserializeObject<dynamic>(filmList);

                // Extract the films array (assuming it's nested under "results")
                var filmsArray = filmData.results;

                // Now, convert the filmsArray to a list of FilmDto objects
                films = JsonConvert.DeserializeObject<List<FilmDto>>(filmsArray.ToString());
            }

            return films;
        }

        public async Task<IActionResult> Index()
        {
            for (int page = 1; page <= 50; page++)
            {
                var filmDtos = await GetFilms(page);

                foreach (FilmDto filmDto in filmDtos)
                {
                    var existingFilm = _unitOfWork.Film.Get(f => f.Id == filmDto.Id);
                    if (existingFilm == null)
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
                    }
                    else
                    {
                        existingFilm.Title = filmDto.Title;
                        existingFilm.Overview = filmDto.Overview;
                        existingFilm.Adult = filmDto.Adult;
                        existingFilm.Backdrop_Path = filmDto.Backdrop_Path;
                        existingFilm.Original_Title = filmDto.Original_Title;
                        existingFilm.Original_Language = filmDto.Original_Language;
                        existingFilm.Popularity = filmDto.Popularity;
                        existingFilm.Poster_Path = filmDto.Poster_Path;
                        existingFilm.Vote_Average = filmDto.Vote_Average;
                        existingFilm.Vote_Count = filmDto.Vote_Count;
                        existingFilm.Release_Date = filmDto.Release_Date;

                        _unitOfWork.Film.Update(existingFilm);
                    }
                }

                _unitOfWork.Save();

                foreach (var filmDto in filmDtos)
                {
                    var existingFilm = _unitOfWork.Film.Get(f => f.Id == filmDto.Id);
                    if ( existingFilm != null && filmDto.Genre_Ids != null)
                    {
                        foreach (var genreId in filmDto.Genre_Ids)
                        {   
                            var existingGenre = _unitOfWork.Genre.Get(g => g.Id == genreId);
                            if (existingGenre != null)
                            {
                                // Check if the relationship already exists
                                var existingFilmGenre = _unitOfWork.FilmGenre.Get(fg => fg.FilmId == existingFilm.Id && fg.GenreId == genreId);
                                if (existingFilmGenre == null)
                                {
                                    // Add the relationship between film and genre if it does not exist
                                    var filmGenre = new FilmGenre
                                    {
                                        FilmId = existingFilm.Id,
                                        GenreId = genreId
                                    };
                                    _unitOfWork.FilmGenre.Add(filmGenre);
                                }
                            }
                        }
                    }
                }

                _unitOfWork.Save();
            }
            var filmsList = _unitOfWork.Film.GetAll();
            return View(filmsList);
        }
    }
}
