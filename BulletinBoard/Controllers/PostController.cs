using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Controllers
{
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController( IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //[Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public IActionResult AddNewPost(Category type)
        {
            //RedirectToAction("AddNewAnimal", "Animal");
            return RedirectToAction("AddNew" + type.Categorie.ToString(), type.Categorie.ToString());

        }

        //[HttpPost]
        //[Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> AddNewPost(PostVM newPost)
        {
            //string folder = "books/cover";
            //folder += User.FindFirstValue(ClaimTypes.NameIdentifier) + newPost.ImageFile.FileName;
            //string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath , folder);

            //await newPost.ImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            //if (ModelState.IsValid)
            //{
            //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    await _service.AddAsync(newPost, userId);
            //    return RedirectToAction("Index", "Home");
            //}
            //TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(newPost);
        }

        [Authorize]
        public async Task<IActionResult> Attribute()
        {
            return View(new Category());
        }
    }
}
