using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;

namespace MovieSearch.Areas.Viewer.Controllers
{
    [Area("Viewer")]
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    
            // Check if the user is logged in
            if (userId == null)
            {
                return Unauthorized(); // or redirect to login page
            }

            // Get all reviews for the logged-in user
            var reviews = _unitOfWork.Review.GetAllByParameters(r => r.UserId == userId, includeProperties: "Film,User");

            // Pass the list of reviews to the view
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Details(int reviewId)
        {
            var review = _unitOfWork.Review.Get(r => r.Id == reviewId, includeProperties: "Film,User");
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }
        [HttpGet]
        public IActionResult Create(int filmId, string userId)
        {  
            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "Film ID and User ID are required.";
                return RedirectToAction("Index","Home");
            }

            Review review = new Review
            {
                FilmId = filmId,
                UserId = userId
            };
            return View(review);
        }
        [HttpPost]
        public IActionResult Create(Review obj)
        {
            if(obj.TextReview.ToLower() == obj.Headline.ToLower())
            {
                ModelState.AddModelError("headline","Назва рецензії та сама рецензія не мають бути однаковими");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Review.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Review created successfully";
            return RedirectToAction("Index", "Home");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Review reviewFromDb = _unitOfWork.Review.Get(u=>u.Id == id);
            return View(reviewFromDb);
        }
        
        
        [HttpPost]
        public IActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Review.Update(review);
                _unitOfWork.Save();
                TempData["success"] = "Review edited successfully";
                return RedirectToAction("Index");
            }
            return View(review);
        }
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            Review reviewFromDb = _unitOfWork.Review.Get(u => u.Id == id);
            return View(reviewFromDb);
        }
        
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Review obj = _unitOfWork.Review.Get(u => u.Id == id);
           
            _unitOfWork.Review.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Review deleted successfully";
            return RedirectToAction("Index");
        }
    }
}