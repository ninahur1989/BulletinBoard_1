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
            return RedirectToAction("AddNew" + type.ThisCategory.ToString(), type.ThisCategory.ToString());

        }

        [Authorize]
        public async Task<IActionResult> ChooseAttribute()
        {
            return View(new AttributeCategory());
        }
    }
}

