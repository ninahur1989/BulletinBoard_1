using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalService _service;

        public AnimalController(IAnimalService service)
        {
            _service = service;
        }

        [Authorize]
        public async Task<IActionResult> AddNewAnimal()
        {
            return View(new AnimalAttribute());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAnimal(AnimalAttribute model)
        {
            await _service.AddAsync(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index", "Home");
        }
    }
}
