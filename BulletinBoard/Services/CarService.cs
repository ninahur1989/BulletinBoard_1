using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class CarService : ICategoryService<CarAttributeVM, CarAttribute>
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarAttribute>> GetAllAsync()
        {
            var carPosts = await _context.CarsAttribute.Where(x => x.MainPost.MainPost.IsEnable == true).ToListAsync();
            return carPosts;
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
                    },
                    PostStatusId = (int)PostStatuses.Active,
                    AttributeCategoryId = (int)Categories.Car
                };

                await _context.AddAsync(newItem);
                user.UserPostList.Add(newItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EditAsync(CarAttributeVM model)
        {
            var carPost = await _context.CarsAttribute.FirstOrDefaultAsync(x => x.MainPost.MainPost.Id == model.Id);

            if (carPost != null)
            {
                carPost.GraduationYear = model.GraduationYear;
                carPost.MileagesCar = model.MileagesCar;
                carPost.VINNumber = model.VINNumber;
                carPost.MainPost.MainPost.Titile = model.Post.Titile;
                carPost.MainPost.MainPost.Description = model.Post.Description;
                carPost.MainPost.MainPost.Price = model.Post.Price;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CarAttributeVM> GetVMAsync(int id, string userId)
        {
            var carPost = await _context.CarsAttribute.FirstOrDefaultAsync(x => x.MainPost.MainPost.Id == id && x.MainPost.MainPost.UserId == userId);

            if (carPost != null)
            {
                return new CarAttributeVM()
                {
                    Id = carPost.Id,
                    VINNumber = carPost.VINNumber,
                    GraduationYear = carPost.GraduationYear,
                    MileagesCar = carPost.MileagesCar,
                    Post = new PostVM()
                    {
                        Description = carPost.MainPost.MainPost.Description,
                        Price = carPost.MainPost.MainPost.Price,
                        Titile = carPost.MainPost.MainPost.Titile
                    }
                };
            }

            return null;
        }
    }
}
