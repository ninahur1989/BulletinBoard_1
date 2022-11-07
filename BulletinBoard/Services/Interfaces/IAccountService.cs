using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<List<Post>> GetMyPosts(string userId);
    }
}
