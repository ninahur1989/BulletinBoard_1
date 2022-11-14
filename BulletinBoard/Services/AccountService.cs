using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BulletinBoard.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetMyPostsAsync(string userId, PostStatuses status)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return user.UserPostList.Where(x => x.Status.Status == status).ToList();
            }

            return new List<Post>();
        }

        public async Task<List<Post>> GetFavoritesAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var postList = new List<Post>();
                foreach (var a in user.FavoritePostList)
                {
                    var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == a.PostId);
                    if (post != null)
                    {
                        postList.Add(post);
                    }
                }
                return postList;
            }

            return new List<Post>();
        }
    }
}
