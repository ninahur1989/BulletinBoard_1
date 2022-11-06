using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Controllers
{
    public class CarController : Controller
    {
        private readonly ICategoryService<CarAttributeVM> _service;

        public CarController(ICategoryService<CarAttributeVM> service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult AddNewCar()
        {
            return View(new CarAttributeVM());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCar(CarAttributeVM model)
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
