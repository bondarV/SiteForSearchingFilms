using Microsoft.AspNetCore.Mvc;
using MovieSearch.Data;
using MovieSearch.Models;

namespace MovieSearch.Controllers
{
    public class GenreController(ApplicationDbContext db) : Controller
    {
        public IActionResult Index()
        {
            List<Genre> objCategoryList = db.Genres.ToList();
            return View(objCategoryList);
        }
    }
}
