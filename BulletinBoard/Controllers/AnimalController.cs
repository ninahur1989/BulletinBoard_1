using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Drawing;
using BulletinBoard.Data.Static;

namespace BulletinBoard.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ICategoryService<AnimalAttributeVM, AnimalAttribute> _service;

        public AnimalController(ICategoryService<AnimalAttributeVM, AnimalAttribute> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetAllAsync();

            if (posts == null)
                return NotFound();
            return View(posts);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new AnimalAttributeVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimalAttributeVM model)
        {
            if (model.Post.ImageFile.Count == 0 || model.Post.ImageFile.Count > ImageLimit.ImageLimitPerPost)
                return View(model);

            if (ModelState.IsValid)
            {
                await _service.AddAsync(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var animalAttributeVM = await _service.GetVMAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (animalAttributeVM == null)
                return NotFound();

            return View(animalAttributeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnimalAttributeVM model)
        {
            if (model.Post.ExistedImage == null && model.Post.ImageFile == null)
                return NoContent();

            if (ModelState.IsValid)
            {
                bool result = await _service.EditAsync(model);

                if (result)
                    return RedirectToAction("Index", "Home");

                return View(model);
            }
            return View(model);
        }
    }
}
