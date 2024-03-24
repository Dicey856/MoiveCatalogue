using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalogue.Data;
using MoviesCatalogue.Models;
using MoviesCatalogue.Repository.IRepository;

namespace MoviesCatalogue.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category resultcategory = _unitOfWork.Category.Get(u => u.Id == id);
            if (resultcategory == null)
            {
                return NotFound();
            }
            return View(resultcategory);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category resultcategory = _unitOfWork.Category.Get(u => u.Id == id);
            if (resultcategory == null)
            {
                return NotFound();
            }
            return View(resultcategory);
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]

        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Delete(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
