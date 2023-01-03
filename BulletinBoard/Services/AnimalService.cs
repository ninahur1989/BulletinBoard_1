using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.Helpers;
using BulletinBoard.Data.Static;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Drawing;
using System.Net.Http.Headers;

namespace BulletinBoard.Services
{
    public class AnimalService : ICategoryService<AnimalAttributeVM, AnimalAttribute>
    {
        private readonly IFileService _fileService;
        private readonly AppDbContext _context;
        private readonly IImageFormHelper _imageFormHelper;

        public AnimalService(AppDbContext context, IFileService fileService, IImageFormHelper imageFormHelper)
        {
            _context = context;
            _fileService = fileService;
            _imageFormHelper = imageFormHelper;
        }

        public async Task AddAsync(AnimalAttributeVM item, string userId)
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
                        AnimalAttribute = new AnimalAttribute()
                        {
                            Age = item.Age
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

        public async Task<bool> EditAsync(AnimalAttributeVM model)
        {
            var animalPost = await _context.AnimalsAttribute.FirstOrDefaultAsync(x => x.MainPost.MainPost.Id == model.Id);

            if (animalPost != null)
            {
                animalPost.Age = model.Age;
                animalPost.MainPost.MainPost.Titile = model.Post.Titile;
                animalPost.MainPost.MainPost.Description = model.Post.Description;
                animalPost.MainPost.MainPost.Price = model.Post.Price;

                await _imageFormHelper.CheckExistedImagesAsync(animalPost.MainPost.MainPost, model.Post, _context, _fileService);
                return true;
            }

            return false;
        }

        public async Task<Models.PagedList<AnimalAttribute>> GetAllAsync(int pageNumber , int? minAge, int? maxAge)
        {
            var animalPosts= new List<AnimalAttribute>();
            if (minAge!= null && maxAge != null)
            {
                 animalPosts = await _context.AnimalsAttribute.Skip((pageNumber - 1) * PageInfo.PageSize).Take(PageInfo.PageSize).
               Where(x => x.MainPost.MainPost.IsEnable == true).Where(x=>x.Age <= maxAge && x.Age>minAge).ToListAsync();
            }
            else
            {
                 animalPosts = await _context.AnimalsAttribute.Skip((pageNumber - 1) * PageInfo.PageSize).Take(PageInfo.PageSize).
               Where(x => x.MainPost.MainPost.IsEnable == true).ToListAsync();
            }

            if(animalPosts.Count == 0)
                return null;

            double animalPostsCount = Math.Ceiling((double)_context.AnimalsAttribute.Where(x => x.MainPost.MainPost.IsEnable == true).Count() / PageInfo.PageSize);
            var pagedPosts = new Models.PagedList<AnimalAttribute>(animalPosts, pageNumber, animalPostsCount);
            return pagedPosts;
        }

        public async Task<AnimalAttributeVM> GetVMAsync(int id, string userId)
        {
            var animalPost = await _context.AnimalsAttribute.FirstOrDefaultAsync(x => x.MainPost.MainPost.Id == id && x.MainPost.MainPost.UserId == userId);

            if (animalPost != null)
            {
                var vm = new AnimalAttributeVM()
                {
                    Id = animalPost.Id,
                    Age = animalPost.Age,
                    Post = new PostVM()
                    {
                        Description = animalPost.MainPost.MainPost.Description,
                        Price = animalPost.MainPost.MainPost.Price,
                        Titile = animalPost.MainPost.MainPost.Titile,
                    },
                };

                vm.Post.ExistedImage = _imageFormHelper.ImageToFormFile(animalPost.MainPost.MainPost);
                return vm;
            }

            return new AnimalAttributeVM();
        }
    }
}
