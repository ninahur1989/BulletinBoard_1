using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Controllers
{
    public class CarController : Controller
    {
        private readonly ICategoryService<CarAttribute> _service;

        public CarController(ICategoryService<CarAttribute> service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult AddNewCar()
        {
            return View(new CarAttribute());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCar(CarAttribute model)
        {
            await _service.AddAsync(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index", "Home");
        }
    }
}
