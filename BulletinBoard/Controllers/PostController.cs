using BulletinBoard.Data.Enums;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Controllers
{
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPostService _postService;

        public PostController(IWebHostEnvironment webHostEnvironment, IPostService postService)
        {
            _webHostEnvironment = webHostEnvironment;
            _postService = postService;
        }

        //[Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public IActionResult AddNewPost(AttributeCategory type)
        {
            return RedirectToAction("AddNew" + type.ThisCategory.ToString(), type.ThisCategory.ToString());

        }

        [Authorize]
        public ActionResult ChooseAttribute()
        {
            return View(new AttributeCategory());
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
    }
}

