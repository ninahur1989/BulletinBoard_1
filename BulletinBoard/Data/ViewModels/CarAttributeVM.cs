using BulletinBoard.Data.Enums;
using BulletinBoard.Models.AttributeModels;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Data.ViewModels
{
    public class CarAttributeVM
    {
        public CarAttributeVM()
        {
            Category = new AttributeCategory() { ThisCategory = Categories.Car };
            CategoryId = (int)Categories.Car;
        }

        [Required(ErrorMessage = "GraduationYear is required")]
        public int GraduationYear { get; set; }

        [Required(ErrorMessage = "VINNumber is required")]
        public int VINNumber { get; set; }

        [Required(ErrorMessage = "MileagesCar is required")]
        public float MileagesCar { get; set; }

        public int CategoryId { get; set; }
        public AttributeCategory Category { get; }

        public PostVM Post { get; set; }

    }
}
