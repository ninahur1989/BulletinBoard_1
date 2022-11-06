using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class AnimalAttribute : IAttribute
    {
        public AnimalAttribute()
        {
            Category = new AttributeCategory() { ThisCategory = Categories.Animal };
            AnimalAttributeId = (int)Categories.Animal;
        }

        public int Id { get; set; }
        public byte Age { get; set; }

        public int AnimalAttributeId { get; set; }
        public AttributeCategory Category { get;  }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}
