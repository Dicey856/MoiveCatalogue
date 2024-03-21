using Microsoft.AspNetCore.Mvc;
using MoviesCatalogue.Data;
using MoviesCatalogue.Models;

namespace MoviesCatalogue.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationContext _db;
        public CategoryController(ApplicationContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();  
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null) 
            {
                return NotFound();
            }
            Category resultcategory = _db.Categories.Find(id);
            if(resultcategory == null)
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
            Category resultcategory = _db.Categories.Find(id);
            if (resultcategory == null)
            {
                return NotFound();
            }
            return View(resultcategory);
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
   
            return View();
        }

        [HttpPost]

        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            
            _db.Categories.Remove(obj); 
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
