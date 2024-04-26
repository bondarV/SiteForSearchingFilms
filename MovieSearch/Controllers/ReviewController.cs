using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieSearch.Data;
using MovieSearch.Models;

namespace MovieSearch.Controllers
{

    public class ReviewController(ApplicationDbContext _db) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Review?> objReviewList = _db.Reviews.ToList();
            return View(objReviewList);
        } 
        [HttpPost]
        public IActionResult Create(Review? obj)
        {
            if (obj.TextReview.ToLower() == obj.Headline.ToLower())
            {
                ModelState.AddModelError("headline","Назва рецензії та сама рецензія не мають бути однаковими");
            }
            if (ModelState.IsValid)
            {
                _db.Reviews.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Review created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Review? reviewFromDb = _db.Reviews.Find(id);
            if (reviewFromDb == null)
            {
                return NotFound();
            }
            return View(reviewFromDb);
        }
        
        [HttpPost]
        public IActionResult Edit(Review obj)
        {
            if (ModelState.IsValid)
            {
                _db.Reviews.Update( obj);
                _db.SaveChanges();
                TempData["success"] = "Review edited successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            Review? reviewFromDb = _db.Reviews.Find(id);
            if (reviewFromDb == null)
            {
                return NotFound();
            }
            return View(reviewFromDb);
        }
        
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Review ?obj = _db.Reviews.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Reviews.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Review deleted successfully";
            return RedirectToAction("Index");
        }
    }
}