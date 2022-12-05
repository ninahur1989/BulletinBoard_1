using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;

namespace BulletinBoard.Services.Interfaces
{
    public interface ICategoryService<modelVM, model> where modelVM : class where model : class
    {
        Task AddAsync(modelVM item, string userId);
        Task<bool> EditAsync(modelVM item);
        Task<modelVM> GetVMAsync(int id, string userId);
        Task<PagedList<model>> GetAllAsync(int pageNumber);
    }
}
