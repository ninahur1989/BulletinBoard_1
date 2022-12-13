using Azure.Messaging;
using BulletinBoard.Data;
using BulletinBoard.Models;
using BulletinBoard.Models.NovaPoshtaModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace BulletinBoard.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly AppDbContext _context;

        public OrderController(IOrderService service, AppDbContext context)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index(string warehouse)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postId = HttpContext.Request.Cookies["postId"];
            var city = HttpContext.Request.Cookies["city"];

            var order = await _service.CreateOrder(warehouse, city, userId, int.Parse(postId));

            if (order == null)
            {
                return NotFound();
            }


            return View(order);
        }
        public async Task<IActionResult> CompleteOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                await _service.CompleteOrder(order);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index",order.Warehouse);
        }

        public async Task<IActionResult> ChooseCity(int id)
        {
            if (_context.Posts.FirstOrDefault(x => x.Id == id).UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound("error it is your post");
            }

            HttpContext.Response.Cookies.Append("postId", id.ToString());
            return View(await _service.GetAllCities());
        }

        public async Task<IActionResult> ChooseWarehouse(string city)
        {
            if (city != null)
            {
                HttpContext.Response.Cookies.Append("city", city);
                var warehouses = await _service.GetAllWarehouses(city);

                if (warehouses.Any())
                    return View(await _service.GetAllWarehouses(city));
            }

            return NoContent();
        }
    }
}
