using EntityFrameWork54.Data;
using EntityFrameWork54.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EntityFrameWork54.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private IWebHostEnvironment _webHostEnvironment;
        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ImageRepo repo = new(_connectionString);
            ViewModel viewModel = new()
            {
                Images = repo.GetImages()
            };
            return View(viewModel);
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(IFormFile image, string title)
        {
            var fileName = $"{Guid.NewGuid()} - {image.FileName}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var fileStream = new FileStream(filePath, FileMode.CreateNew);
            image.CopyTo(fileStream);

            ImageRepo repo = new(_connectionString);
            repo.AddImage(new Image
            {
                Title = title,
                FileName = fileName,
                DateUploaded = DateTime.Now,
                Likes = 0
            });
            return RedirectToAction("index");
        }
        public IActionResult ViewImage(int id)
        {
            ImageRepo repo = new(_connectionString);
            List<int> likedImages = HttpContext.Session.Get<List<int>>("Liked");

            ViewModel viewModel = new()
            {
                Image = repo.GetImageById(id)
            };
            if (likedImages != null && likedImages.Contains(id))
            {
                viewModel.CannotLike = true;
            }

            return View(viewModel);
        }
        public void AddLike(int id)
        {
            ImageRepo repo = new(_connectionString);
            repo.AddLike(id);
            List<int> likedImages = HttpContext.Session.Get<List<int>>("Liked") ?? new();
            likedImages.Add(id);
            HttpContext.Session.Set("Liked", likedImages);

        }
        public int GetLikes(int id)
        {
            ImageRepo repo = new(_connectionString);
            return repo.GetImageById(id).Likes;
        }


    }
}