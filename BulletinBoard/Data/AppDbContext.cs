using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Models.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BulletinBoard.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<AttributeCategory> AttributeCategories { get; set; }
        public DbSet<AnimalAttribute> AnimalsAttribute { get; set; }
        public DbSet<CarAttribute> CarsAttribute { get; set; }
        public DbSet<Attribute_Post> Attribute_Posts { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
