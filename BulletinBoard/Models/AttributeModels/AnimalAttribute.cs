using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class AnimalAttribute : IAttribute
    {
        public AnimalAttribute()
        {
            Category = new AttributeCategory() { ThisCategory = Categories.Animal };
            AttributeCategoryId = (int)Categories.Animal;
        }

        public int Id { get; set; }
        public byte Age { get; set; }

        public int AttributeCategoryId { get; set; }
        [ForeignKey(nameof(AttributeCategoryId))]
        public virtual AttributeCategory Category { get; set; }

        public int Attribute_PostId { get; set; }

        [ForeignKey(nameof(Attribute_PostId))]
        public virtual Attribute_Post MainPost { get; set; }
    }
}
