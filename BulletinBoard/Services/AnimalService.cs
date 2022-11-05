using BulletinBoard.Data;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly AppDbContext _context;


        public AnimalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AnimalAttribute animal, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                animal.Post.CreatedDate = DateTime.UtcNow;
                animal.Post.ExpiredDate = DateTime.UtcNow.AddDays(30);
                animal.Post.User = user;
                animal.Post.UserId = userId;
                animal.Post.IsEnable = true;
                animal.Post.Location = user.Location;


                await _context.AddAsync(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
