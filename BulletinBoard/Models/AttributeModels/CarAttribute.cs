using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class CarAttribute : IAttribute
    {
        private Categories _categorie = Categories.Car;
        public int Id { get; set; }

        public int GraduationYear { get; set; }
        public int VINNumber { get; set; }
        public float MileagesCar { get; set; }
        public Categories Categorie { get { return _categorie; } }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}
