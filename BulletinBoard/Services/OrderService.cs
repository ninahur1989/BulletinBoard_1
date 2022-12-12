using BulletinBoard.Data.API.NovaPoshta;
using BulletinBoard.Models.NovaPoshtaModels;
using BulletinBoard.Services.Interfaces;

namespace BulletinBoard.Services
{
    public class OrderService : IOrderService
    {
        private readonly NovaPoshtaProvider _novaPoshtaProvider = new NovaPoshtaProvider();
        public async Task<List<City>> GetAllCities() 
        {
            return await _novaPoshtaProvider.GetCitys();
        }

        public async Task<List<Warehouse>> GetAllWarehouses(string city)
        {
            return await _novaPoshtaProvider.GetWarehouses(city);
        }
    }
}
