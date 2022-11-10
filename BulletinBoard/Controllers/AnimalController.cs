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
        private readonly ICategoryService<AnimalAttributeVM, AnimalAttribute> _service;

        public AnimalController(ICategoryService<AnimalAttributeVM, AnimalAttribute> service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult AddNewAnimal()
        {
            return View(new AnimalAttributeVM());
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetAllAsync();

            if(posts == null)
                return NotFound();
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAnimal(AnimalAttributeVM model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return RedirectToAction("Index", "Home");
            }
            return View( model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var animalAttributeVM = await _service.GetVMAsync(id , User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (animalAttributeVM == null) 
                return NotFound();

            return View(animalAttributeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnimalAttributeVM model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _service.EditAsync(model);

                if(result)
                    return RedirectToAction("Index", "Home");

                return View(model);
            }
            return View(model);
        }
    }
}
