using Microsoft.AspNetCore.Mvc;
using MoviesCatalogue.Models;
using MoviesCatalogue.Repository.IRepository;
using System.Diagnostics;

namespace MoviesCatalogue.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Movie> movielist = _unitOfWork.Movie.GetAll(includeProperties: "Category").ToList();
            return View(movielist);
        }

        public IActionResult Details(int id)
        {
            Movie movie = _unitOfWork.Movie.Get(u => u.Id == id, includeProperties: "Category");
            return View(movie);
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
