using BulletinBoard.Data;
using BulletinBoard.Data.API.NovaPoshta;
using BulletinBoard.Data.Enums;
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

        public async Task RemoveOrderAsync(int orderid)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderid);

            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task CompleteOrderAsync(int orderid)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderid);

            if (order != null)
            {
                order.Status= OrderStatus.Completed;
                await _context.SaveChangesAsync();
            }
        }
        public async Task AcceptOrderAsync(int orderid)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderid);

            if (order != null)
            {
                order.Status = OrderStatus.Accepted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteOrder(Order order)
        {
            try
            {
                order.CreatedDate = DateTime.UtcNow;
                order.Type = OrderType.Purchase;
                order.Status = OrderStatus.Waiting;


                _context.Orders.Add(order);
                await _context.SaveChangesAsync();


                var post = await _context.Posts.FirstOrDefaultAsync(c => c.Id == order.PostId);
                if (post != null)
                {
                    order.UserId = post.UserId;
                    order.Type = OrderType.Order;
                    order.Id = default;
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<Order> CreateOrder(string warehouse, string city, string userId, int postId)
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
                order.Email = user.Email;
                order.PostId = postId;
                order.UserId = userId;

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
