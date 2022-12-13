using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.UserModels
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Your location")]
        public string Location { get; set; }

        public virtual List<Post> UserPostList { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<Favorite> FavoritePostList { get; set; }
    }
}
