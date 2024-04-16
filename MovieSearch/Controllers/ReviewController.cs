using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieSearch.Data;
using MovieSearch.Models;

namespace MovieSearch.Controllers
{

    public class ReviewController(ApplicationDbContext _db) : Controller
    {
        public IActionResult Index()
        {
            List<Review> objReviewList = _db.Reviews.ToList();
            return View(objReviewList);
        } 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Review obj)
        {
            if (ModelState.IsValid)
            {
                
            }
            _db.Reviews.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}