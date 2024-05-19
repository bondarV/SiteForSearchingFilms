using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model.Models;

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
        public IActionResult Index()
        {   
            var filmList = _unitOfWork.Film.GetAll().OrderByDescending(f => f.Popularity).Take(8);
            return View(filmList);
        }
        public IActionResult Details(int filmId)    
        {   
            var film = _unitOfWork.Film.Get(u=> u.Id == filmId);
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
