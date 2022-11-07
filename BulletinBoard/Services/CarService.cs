using BulletinBoard.Data;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class CarService : ICategoryService<CarAttributeVM>
    {
        private readonly AppDbContext _context;


        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CarAttributeVM car, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var newItem = new Post()
                {
                    Price = car.Post.Price,
                    Description = car.Post.Description,
                    Titile = car.Post.Titile,
                    CreatedDate = DateTime.UtcNow,
                    ExpiredDate = DateTime.UtcNow.AddDays(30),
                    User = user,
                    UserId = userId,
                    IsEnable = true,
                    Location = user.Location,
                    MainAttribute_Post = new Attribute_Post()
                    {
                        CarAttribute = new CarAttribute()
                        {
                            MileagesCar = car.MileagesCar,
                            VINNumber = car.VINNumber,
                            GraduationYear = car.GraduationYear,
                        }
                    }

                };

                //car.Post.CreatedDate = DateTime.UtcNow;
                //car.Post.ExpiredDate = DateTime.UtcNow.AddDays(30);
                //car.Post.User = user;
                //car.Post.UserId = userId;
                //car.Post.IsEnable = true;
                //car.Post.Location = user.Location;



                await _context.AddAsync(newItem);
                user.Posts.Add(newItem);
                await _context.SaveChangesAsync();
            }
        }

    }
}
