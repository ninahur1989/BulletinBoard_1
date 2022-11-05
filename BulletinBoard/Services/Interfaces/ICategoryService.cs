using BulletinBoard.Models.AttributeModels;

namespace BulletinBoard.Services.Interfaces
{
    public interface ICategoryService<T> where T : class,IAttribute
    {
        Task AddAsync(T item, string userId);
    }
}
