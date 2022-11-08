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

            var posts = await _context.Posts.Where(x => x.Titile.ToLower().StartsWith(searchTitile)  && x.Location.ToLower().StartsWith(searchLocation)).ToListAsync();
            return posts;
        }
    }
}
