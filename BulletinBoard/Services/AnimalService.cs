using BulletinBoard.Data;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class AnimalService : ICategoryService<AnimalAttribute> 
    {
        private readonly AppDbContext _context;


        public AnimalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AnimalAttribute item, string userId) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                item.Post.CreatedDate = DateTime.UtcNow;
                item.Post.ExpiredDate = DateTime.UtcNow.AddDays(30);
                item.Post.User = user;
                item.Post.UserId = userId;
                item.Post.IsEnable = true;
                item.Post.Location = user.Location;


                await _context.AddAsync(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}
