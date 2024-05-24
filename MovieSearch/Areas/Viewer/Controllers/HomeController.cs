using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(string searchString)
        {   var films = _unitOfWork.Film.GetAll();
             films = films.OrderByDescending(film => film.Popularity).Take(16).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                films = _unitOfWork.Film.GetAll();
                films = films.Where(f => 
                    f.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    f.Original_Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }
            /*else
            {
                films = films.OrderByDescending(film => film.Popularity).Take(16).ToList();
            }*/
            return View(films);
        }

        public IActionResult Details(int filmId)    
        {   
            var film = _unitOfWork.Film.Get(u=> u.Id == filmId, includeProperties: "FilmGenres.Genre");
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
