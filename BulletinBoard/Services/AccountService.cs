using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.ViewModels;
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

        public async Task<List<Post>> GetMyPostsAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return user.UserPostList.ToList();
            }

            return new List<Post>();
        }
    }
}
