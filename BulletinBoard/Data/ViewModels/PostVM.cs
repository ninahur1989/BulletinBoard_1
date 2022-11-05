
using BulletinBoard.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Data.ViewModels
{
    public class PostVM
    {
        [Required(ErrorMessage = "Titile is required")]
        public string Titile { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(80), MaxLength(9000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "PostCategories is required")]
        public Categories PostCategories { get; set; }

        //[DisplayName("Upload File")]
        //public IFormFile? ImageFile { get; set; }
    }
}
