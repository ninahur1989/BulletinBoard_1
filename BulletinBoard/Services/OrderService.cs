using BulletinBoard.Data;
using BulletinBoard.Data.API.NovaPoshta;
using BulletinBoard.Models;
using BulletinBoard.Models.NovaPoshtaModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly NovaPoshtaProvider _novaPoshtaProvider;

        public OrderService(AppDbContext context, IConfiguration configuration)
        {
            _novaPoshtaProvider = new NovaPoshtaProvider(configuration);
            _context = context;
        }

        public async Task<List<City>> GetAllCities()
        {
            return await _novaPoshtaProvider.GetCitys();
        }

        public async Task<Order> CreateOrder(string warehouse, string userId, string city)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                Order order = new Order();
                order.Warehouse = warehouse;
                order.FirstName = user.UserName;
                order.LastName = user.FullName;
                order.City = city;  
                order.PhoneNumber = user.PhoneNumber;
                order.Email= user.Email;
                return order;
            }
            return null;
        }

        public async Task<List<Warehouse>> GetAllWarehouses(string city)
        {
            return await _novaPoshtaProvider.GetWarehouses(city);
        }
    }
}
