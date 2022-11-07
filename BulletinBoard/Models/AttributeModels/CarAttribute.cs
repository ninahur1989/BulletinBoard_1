using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class CarAttribute : IAttribute
    {
        public int Id { get; set; }

        public int GraduationYear { get; set; }

        public int VINNumber { get; set; }

        public float MileagesCar { get; set; }

        public int Attribute_PostId { get; set; }
        [ForeignKey(nameof(Attribute_PostId))]
        public virtual Attribute_Post MainPost { get; set; }

    }
}
