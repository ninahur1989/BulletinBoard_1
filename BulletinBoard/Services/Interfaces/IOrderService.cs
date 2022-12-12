using BulletinBoard.Data.API.NovaPoshta;
using BulletinBoard.Models.NovaPoshtaModels;

namespace BulletinBoard.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<City>> GetAllCities();

        public Task<List<Warehouse>> GetAllWarehouses(string city);

    }
}
