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
    }
}
