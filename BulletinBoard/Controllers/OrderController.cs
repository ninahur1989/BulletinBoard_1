using BulletinBoard.Models.NovaPoshtaModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Text;

namespace BulletinBoard.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ChooseCity()
        {
            return View(await _service.GetAllCities());
        }

        public async Task<IActionResult> ChooseWarehouse(string city)
        {
            var warehouses = await _service.GetAllWarehouses(city);

            if(warehouses.Any())
                return View(await _service.GetAllWarehouses(city));

            return NoContent();
        }
    }
}
