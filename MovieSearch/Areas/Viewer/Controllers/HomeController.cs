using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;
using NuGet.Protocol;

namespace MovieSearch.Areas.Viewer.Controllers
{
    [Area("Viewer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string? searchString, int? genre, string? sortBy, int countFilmOnPage = 4)
        {
            ViewBag.SearchString = searchString;
            ViewBag.GenreSelectedValue = genre;
            ViewBag.SortBySelectedValue = sortBy;
            ViewBag.CountFilmOnPageSelectedValue = countFilmOnPage;
            var films = _unitOfWork.Film.GetAll(includeProperties: "FilmGenres.Genre");

            if (!string.IsNullOrEmpty(searchString))
            {
                films = films.Where(f => 
                    f.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    f.Original_Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }
            if (genre.HasValue && genre.Value != 0)
            {
                films = films
                    .Where(f => f.FilmGenres.Any(fg => fg.GenreId == genre.Value))
                    .ToList();
            }

            switch (sortBy)
            {
                case "popularity":
                    films = films.OrderByDescending(film => film.Popularity).ToList();
                    break;
                case "vote_average":
                    films = films.OrderByDescending(film => film.Vote_Average).ToList();
                    break;
                case "vote_count":
                    films = films.OrderByDescending(film => film.Vote_Count).ToList();
                    break;
                default:
                    films = films.OrderByDescending(film => film.Popularity).ToList(); // Default sorting by popularity
                    break;
            }
            var genres = _unitOfWork.Genre.GetAll();
            ViewBag.Genres = new SelectList(genres, "Id", "Name", genre);
          
          
            ViewBag.SortByOptions = new SelectList(new[]
            {
                new { Text = "Popularity", Value = "popularity" },
                new { Text = "Vote Average", Value = "vote_average" },
                new { Text = "Vote Count", Value = "vote_count" }
            }, "Value", "Text", sortBy);
            
            films = films.Take(countFilmOnPage).ToList();
            return View(films);
        }

        public IActionResult Details(int filmId)
        {
            var film = _unitOfWork.Film.Get(u => u.Id == filmId , includeProperties: "FilmGenres.Genre,Reviews.User");
            return View(film);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
