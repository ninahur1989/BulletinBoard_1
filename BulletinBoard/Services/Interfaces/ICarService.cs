using BulletinBoard.Models.AttributeModels;

namespace BulletinBoard.Services.Interfaces
{
    public interface ICarService
    {
        Task AddAsync(CarAttribute car, string userId);
    }
}
