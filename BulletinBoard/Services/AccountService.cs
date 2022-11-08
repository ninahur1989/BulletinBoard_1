using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Models;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<Post?> GetPostByCategory(Categories? category, int id)
        {
            switch (category)
            {
                case Categories.Animal:
                    var animalPost = await _context.AnimalsAttribute.FirstOrDefaultAsync(x => x.MainPost.Id == id);
                    return animalPost.MainPost.MainPost;
                case Categories.Car:
                    var carPost = await _context.CarsAttribute.FirstOrDefaultAsync(x => x.MainPost.Id == id);
                    return carPost.MainPost.MainPost;
                default: return null;
            }
        }

        public async Task<List<Post>> GetMyPostsAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return user.Posts.ToList();
            }

            return new List<Post>();
        }

        public async Task<Post?> GetPostByIdAsync(int id, string? userId, Categories? category)
        {
            if (userId != null)
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
                if (user != null)
                {
                    var deteils = user.Posts.FirstOrDefault(x => x.Id == id);
                    return deteils;
                }
            }

            if (category != null)
            {
                var deteils = await GetPostByCategory(category, id);
                return deteils;
            }

            var postDetails = _context.Posts.FirstOrDefault(x => x.Id == id);
            return postDetails;
        }
    }
}
