using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ICategoryService<AnimalAttributeVM> _service;

        public AnimalController(ICategoryService<AnimalAttributeVM> service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult AddNewAnimal()
        {
            return View(new AnimalAttributeVM());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAnimal(AnimalAttributeVM model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
