using BulletinBoard.Data.Static;
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
        private int _pageIndex = PageInfo.PageIndex;
        private readonly ICategoryService<CarAttributeVM, CarAttribute> _service;

        public CarController(ICategoryService<CarAttributeVM, CarAttribute> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            _pageIndex = pageNumber.HasValue ? Convert.ToInt32(pageNumber.Value) : 1;
            var posts = await _service.GetAllAsync(_pageIndex);

            if (posts == null)
                return NotFound();
            return View(posts);
        }

        [Authorize]
        public IActionResult AddNewCar()
        {
            return View(new CarAttributeVM());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCar(CarAttributeVM model)
        {
            if (model.Post.ImageFile == null || model.Post.ImageFile.Count > ImageLimit.ImageLimitPerPost)
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
            var carAttributeVM = await _service.GetVMAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (carAttributeVM == null)
                return NotFound();

            return View(carAttributeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarAttributeVM model)
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
