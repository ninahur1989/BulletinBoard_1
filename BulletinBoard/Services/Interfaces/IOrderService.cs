using BulletinBoard.Data.API.NovaPoshta;
using BulletinBoard.Models;
using BulletinBoard.Models.NovaPoshtaModels;

namespace BulletinBoard.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<City>> GetAllCities();
        public Task<Order> CreateOrder(string warehouse, string city, string userId, int postId);
        public Task<List<Warehouse>> GetAllWarehouses(string city);
        public Task CompleteOrder(Order order);
        public Task RemoveOrderAsync(int orderid);
        public Task CompleteOrderAsync(int orderid);
        public Task AcceptOrderAsync(int orderid);
    }
}