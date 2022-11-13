using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.Static;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net.Http.Headers;

namespace BulletinBoard.Services
{
    public class AnimalService : ICategoryService<AnimalAttributeVM, AnimalAttribute>
    {
        private readonly IFileService _fileService;
        private readonly AppDbContext _context;


        public AnimalService(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
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

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<AnimalAttribute>> GetAllAsync()
        {
            var animalPosts = await _context.AnimalsAttribute.Where(x => x.MainPost.MainPost.IsEnable == true).ToListAsync();
            return animalPosts;
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
                        Images = animalPost.MainPost.MainPost.Images,
                        ExistedImage = new List<IFormFile?>(),
                    },
                };

                foreach (var a in vm.Post.Images)
                {
                    string path = "./wwwroot/uploads/" + a.Name;
                    using (var stream = System.IO.File.OpenRead(path))
                    {
                        vm.Post.ExistedImage.Add(new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)));
                    }
                }
                return vm;
            }

            return null;
        }
    }
}
