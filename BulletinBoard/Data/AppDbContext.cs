using BulletinBoard.Models;
using BulletinBoard.Models.AttributeModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categorys { get; set; }

        public DbSet<AnimalAttribute> AnimalAttributes { get; set; }
        public DbSet<CarAttribute> CarAttributes { get; set; }
    }
}
