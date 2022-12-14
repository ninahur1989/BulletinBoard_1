using BulletinBoard.Data.Enums;
using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<List<Post>> GetMyPostsAsync(string userId, PostStatuses status);
        public Task<List<Post>> GetFavoritesAsync(string userId);
        public Task<List<Order>> GetOrdersAsync(string userId, OrderType type);
    }
}
