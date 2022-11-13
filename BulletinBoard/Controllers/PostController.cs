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

        public async Task<IActionResult> Index(string? searchTitile, string? searchLocation)
        {
            var posts = await _postService.SearchPostAsync(searchTitile, searchLocation);

            if (posts == null)
                return NotFound();

            return View(posts);
        }

        //[Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public IActionResult AddNewPost(AttributeCategory type)
        {
            return RedirectToAction("AddNew" + type.Category.ToString(), type.Category.ToString());

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
                return NotFound();

            return View(post);
        }

        public IActionResult Edit(int id, Categories category)
        {
            return RedirectToAction("Edit", category.ToString(), new { @id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Deactivate(int id)
        {
            await _postService.DeactivatePostAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Activate(int id)
        {
            await _postService.ActivatePostAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index", "Account");
        }
    }
}

