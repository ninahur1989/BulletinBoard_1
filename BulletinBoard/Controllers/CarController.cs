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
        private readonly ICategoryService<CarAttributeVM, CarAttribute> _service;

        public CarController(ICategoryService<CarAttributeVM, CarAttribute> service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult AddNewCar()
        {
            return View(new CarAttributeVM());
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetAllAsync();

            if (posts == null)
                return NotFound();
            return View(posts);
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

        public async Task<IActionResult> Edit(int id)
        {
            var carAttributeVM = await _service.GetVMAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (carAttributeVM == null)
                return NotFound();

            return View(carAttributeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarAttributeVM model)
        {
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
