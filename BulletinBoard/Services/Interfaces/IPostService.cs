using BulletinBoard.Data.Enums;
using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IPostService
    {
        public Task<Post?> GetPostByIdAsync(int id);
    }
}
