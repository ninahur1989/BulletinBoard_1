using BulletinBoard.Models.AttributeModels;

namespace BulletinBoard.Services.Interfaces
{
    public interface IAnimalService
    {
        Task AddAsync(AnimalAttribute animal, string userId);
    }
}
