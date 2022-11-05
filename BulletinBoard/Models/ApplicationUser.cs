using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Your location")]
        public string Location { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
