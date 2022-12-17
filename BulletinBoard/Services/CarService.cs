using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.Helpers;
using BulletinBoard.Data.Static;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Services
{
    public class CarService : ICategoryService<CarAttributeVM, CarAttribute>
    {
        private readonly IFileService _fileService;
        private readonly AppDbContext _context;
        private readonly IImageFormHelper _imageFormHelper;

        public CarService(AppDbContext context, IFileService fileService, IImageFormHelper imageFormHelper)
        {
            _context = context;
            _fileService = fileService;
            _imageFormHelper = imageFormHelper;
        }

        public async Task<PagedList<CarAttribute>> GetAllAsync(int pageNumber, int? minMilles , int? maxMilles)
        {
            var carPosts = new List<CarAttribute>();
            if (minMilles != null && maxMilles != null)
            {
                carPosts = await _context.CarsAttribute.Skip((pageNumber - 1) * PageInfo.PageSize).Take(PageInfo.PageSize).
              Where(x => x.MainPost.MainPost.IsEnable == true).Where(x => x.MileagesCar <= maxMilles && x.MileagesCar > minMilles).ToListAsync();
            }
            else
            {
                carPosts = await _context.CarsAttribute.Skip((pageNumber - 1) * PageInfo.PageSize).Take(PageInfo.PageSize).
              Where(x => x.MainPost.MainPost.IsEnable == true).ToListAsync();
            }

            if (carPosts.Count == 0)
                return null;

            double animalPostsCount = Math.Ceiling((double)_context.AnimalsAttribute.Where(x => x.MainPost.MainPost.IsEnable == true).Count() / PageInfo.PageSize);
            var pagedPosts = new Models.PagedList<CarAttribute>(carPosts, pageNumber, animalPostsCount);
            return pagedPosts;
        }

        public async Task AddAsync(CarAttributeVM item, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var newItem = new Post()
                {
                    Price = item.Post.Price,
                    Description = item.Post.Description,
                    Titile = item.Post.Titile,
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
                            MileagesCar = item.MileagesCar,
                            VINNumber = item.VINNumber,
                            GraduationYear = item.GraduationYear,
                        }
                    },
                    PostStatusId = (int)PostStatuses.Active,
                    CountInFavorites = default,
                    AttributeCategoryId = (int)Categories.Animal
                };

                newItem.Images = await _fileService.UploadAsync(item.Post.ImageFile, userId);

                user.UserPostList.Add(newItem);

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

                await _imageFormHelper.CheckExistedImagesAsync(carPost.MainPost.MainPost, model.Post, _context, _fileService);
                return true;
            }

            return false;
        }

        public async Task<CarAttributeVM> GetVMAsync(int id, string userId)
        {
            var carPost = await _context.CarsAttribute.FirstOrDefaultAsync(x => x.MainPost.MainPost.Id == id && x.MainPost.MainPost.UserId == userId);

            if (carPost != null)
            {
                var vm = new CarAttributeVM()
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

                vm.Post.ExistedImage = _imageFormHelper.ImageToFormFile(carPost.MainPost.MainPost);
                return vm;
            }

            return new CarAttributeVM();
        }
    }
}
