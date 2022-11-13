using BulletinBoard.Data.Enums;
using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IAccountService 
    {
        public Task<List<Post>> GetMyPostsAsync(string userId, PostStatuses status);

    }
}
