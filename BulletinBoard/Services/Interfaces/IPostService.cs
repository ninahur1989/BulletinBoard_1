using BulletinBoard.Data.Enums;
using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IPostService
    {
        public Task<Post?> GetPostByIdAsync(int id);
        public Task<List<Post?>> SearchPostAsync(string? searchTitile, string? searchLocation);
        public Task DeletePostAsync(int id, string userId);
        public Task DisablePostAsync(int id, string userId);
    }
}
