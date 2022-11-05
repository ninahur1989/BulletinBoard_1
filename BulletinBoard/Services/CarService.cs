using BulletinBoard.Data;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class CarService : ICategoryService<CarAttribute>
    {
        private readonly AppDbContext _context;


        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CarAttribute car, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                car.Post.CreatedDate = DateTime.UtcNow;
                car.Post.ExpiredDate = DateTime.UtcNow.AddDays(30);
                car.Post.User = user;
                car.Post.UserId = userId;
                car.Post.IsEnable = true;
                car.Post.Location = user.Location;


                await _context.AddAsync(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
