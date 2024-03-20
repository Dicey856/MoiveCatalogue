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

    }
}
