using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Models;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<List<Post?>> SearchPostAsync(string? searchTitile, string? searchLocation)
        {
            if (string.IsNullOrWhiteSpace(searchTitile))
                searchTitile = "";

            if (string.IsNullOrWhiteSpace(searchLocation))
                searchLocation = "";

            var posts = await _context.Posts.Where(x => x.IsEnable == true && x.Titile.ToLower().StartsWith(searchTitile) && x.Location.ToLower().StartsWith(searchLocation)).ToListAsync();
            return posts;
        }

        public async Task DeletePostAsync(int id, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var post = user.UserPostList.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DisablePostAsync(int id, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var post = user.UserPostList.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                post.IsEnable = false;
                post.Status.Id = (int)PostStatuses.Inactive;
                await _context.SaveChangesAsync();
            }
        }
    }
}
