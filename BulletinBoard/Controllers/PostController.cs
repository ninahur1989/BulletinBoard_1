using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models.AttributeModels;
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
        public IActionResult AddNewPost(AttributeCategory type)
        {
            //RedirectToAction("AddNewAnimal", "Animal");
            return RedirectToAction("AddNew" + type.ThisCategory.ToString(), type.ThisCategory.ToString());

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
        public async Task<IActionResult> ChooseAttribute()
        {
            return View(new AttributeCategory());
        }
    }
}

