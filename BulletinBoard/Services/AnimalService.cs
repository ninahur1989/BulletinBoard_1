﻿using BulletinBoard.Data;
using BulletinBoard.Data.Enums;
using BulletinBoard.Data.ViewModels;
using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Services
{
    public class AnimalService : ICategoryService<AnimalAttributeVM>
    {
        private readonly AppDbContext _context;


        public AnimalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AnimalAttributeVM item, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var newItem = new AnimalAttribute()
                {
                    Age = item.Age,
                    AnimalAttributeId = (int)Categories.Animal,
                    Post = new Post()
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
                    }
                };

                await _context.AddAsync(newItem);
                user.Posts.Add(newItem.Post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
