using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Models;
using BulletinBoard.Models.UserModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class PostService : IPostService
    {
        private readonly IFileService _fileService;
        private readonly AppDbContext _context;

        public PostService(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
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
                _fileService.Delete(post.Images);
                await DeletePostImageAsync(post.Images);
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePostImageAsync(List<Image> images)
        {
            _context.Images.RemoveRange(images);

            await _context.SaveChangesAsync();
        }

        public async Task DeactivatePostAsync(int id, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var post = user.UserPostList.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                post.IsEnable = false;
                post.PostStatusId = (int)PostStatuses.Inactive;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActivatePostAsync(int id, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var post = user.UserPostList.FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                post.IsEnable = true;
                post.PostStatusId = (int)PostStatuses.Active;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AddFavoriteAsync(int id, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

                if (post != null)
                {
                    var isExisted = await IsFavoriteAsync(id, userId);

                    if (isExisted)
                    {
                        var favoriteDB = user.FavoritePostList.First(x => x.ApplicationUserId == userId && x.PostId == id);
                        _context.Remove(favoriteDB);
                        post.CountInFavorites--;
                        await _context.SaveChangesAsync();
                        return true;
                    }

                    var favorite = new Favorite()
                    {
                        PostId = id,
                        ApplicationUserId = userId,
                    };

                    await _context.AddAsync(favorite);
                    post.CountInFavorites++;
                    await _context.SaveChangesAsync();

                    return false;
                }

                throw new InvalidDataException();
            }

            throw new InvalidDataException();
        }

        public async Task<bool> IsFavoriteAsync(int id, string userId)
        {
            var isExisted = await _context.Favorites.FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.PostId == id);

            if (isExisted == null)
                return false;

            return true;
        }

        public async Task<List<Post>> GetUserPostsAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post != null)
                return post.User.UserPostList.Where(x=>x.IsEnable).ToList();

            return new List<Post>();
        }
    }
}
