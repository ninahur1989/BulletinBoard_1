using BulletinBoard.Data.Enums;
using BulletinBoard.Models.AttributeModels;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Data.ViewModels
{
    public class AnimalAttributeVM
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1,100)]
        public byte Age { get; set; }

        public  PostVM Post { get; set; }
    }
}
