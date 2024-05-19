using Microsoft.AspNetCore.Mvc;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model.Models;

namespace MovieSearch.Areas.Viewer.Controllers
{
    [Area("Viewer")]
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Review> objReviewList = _unitOfWork.Review.GetAll().ToList();
            return View(objReviewList);
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

            Review reviewFromDb = _unitOfWork.Review.Get(u=>u.Id == id);
            return View(reviewFromDb);
        }
        
        [HttpPost]
        public IActionResult Edit(Review obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Review.Update( obj);
                _unitOfWork.Save();
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