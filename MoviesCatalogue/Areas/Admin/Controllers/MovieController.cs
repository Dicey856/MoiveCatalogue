using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesCatalogue.Models;
using MoviesCatalogue.Models.ViewModels;
using MoviesCatalogue.Repository.IRepository;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.WebSockets;

namespace MoviesCatalogue.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;   
        }
        public IActionResult Index()
        {
            List<Movie> objMovieList = _unitOfWork.Movie.GetAll(includeProperties:"Category").ToList();

            return View(objMovieList);
        }

        public IActionResult Upsert(int? id)
        {
            
            MovieViewModel movieVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Movie = new Movie()
            };
            if (id == null || id == 0)
            {
                return View(movieVM);
            }
            else
            {
                movieVM.Movie = _unitOfWork.Movie.Get(u => u.Id == id);
                return View(movieVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(MovieViewModel movieVM, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string moviePath = Path.Combine(wwwRootPath, @"images\movie");

                    if(!string.IsNullOrEmpty(movieVM.Movie.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, movieVM.Movie.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(moviePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    movieVM.Movie.ImageUrl = @"\images\movie\" + fileName;
                }
                if(movieVM.Movie.Id == 0)
                {
                    _unitOfWork.Movie.Add(movieVM.Movie);
                }
                else
                {
                    _unitOfWork.Movie.Update(movieVM.Movie);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                movieVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(movieVM);
            }
            
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Movie resultmovie = _unitOfWork.Movie.Get(u => u.Id == id);
            if (resultmovie == null)
            {
                return NotFound();
            }
            return View(resultmovie);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Movie? obj = _unitOfWork.Movie.Get(u => u.Id == id);
            if(obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Movie.Delete(obj);
            _unitOfWork.Movie.Save();
            return RedirectToAction("Index");
        }
    }
}
