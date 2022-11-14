using BulletinBoard.Data.Enums;
using BulletinBoard.Models;

namespace BulletinBoard.Services.Interfaces
{
    public interface IPostService
    {
        public Task<Post?> GetPostByIdAsync(int id);
        public Task<List<Post?>> SearchPostAsync(string? searchTitile, string? searchLocation);
        public Task DeletePostAsync(int id, string userId);
        public Task DeactivatePostAsync(int id, string userId);
        public Task ActivatePostAsync(int id, string userId);
        public Task DeletePostImageAsync(List<Image> images);
        public Task<bool> AddFavoriteAsync(int id, string userId);
    }
}
