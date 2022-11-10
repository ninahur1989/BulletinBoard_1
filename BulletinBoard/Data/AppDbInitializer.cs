using BulletinBoard.Data.Static;
using BulletinBoard.Models;
using Microsoft.AspNetCore.Identity;
using BulletinBoard.Data.Enums;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Models.UserModels;

namespace BulletinBoard.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.AttributeCategories.Any())
                {
                    for (int i = 1; i <= Enum.GetNames(typeof(Categories)).Length; i++)
                    {
                        var category = new AttributeCategory { Category = (Categories)i, CategoryName = Enum.GetName(typeof(Categories), i), Id = i };
                        context.Add(category);
                    }
                    context.SaveChanges();
                }
                if (!context.PostStatuses.Any())
                {
                    for (int i = 1; i <= Enum.GetNames(typeof(PostStatuses)).Length; i++)
                    {
                        var status = new PostStatus { Status = (PostStatuses)i, StatusName = Enum.GetName(typeof(PostStatuses), i), Id = i };
                        context.Add(status);
                    }
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Location = "Main office, KYIV",
                    };
                    await userManager.CreateAsync(newAdminUser, "$%32gdsDSs2");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Location = "Main office, KYIV",
                    };

                    await userManager.CreateAsync(newAppUser, "$%32gdsDSs2");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
