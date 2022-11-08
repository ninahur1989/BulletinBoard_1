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
    }
}
