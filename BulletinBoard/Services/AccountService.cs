using BulletinBoard.Data;
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

        public async Task<List<Post>> GetMyPosts(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return user.Posts.ToList();
            }

            return new List<Post>();
        }
    }
}
