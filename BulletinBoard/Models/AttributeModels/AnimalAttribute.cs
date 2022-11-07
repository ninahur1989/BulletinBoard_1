using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class AnimalAttribute : IAttribute
    {
        public int Id { get; set; }
        public byte Age { get; set; }

        public int Attribute_PostId { get; set; }

        [ForeignKey(nameof(Attribute_PostId))]
        public virtual Attribute_Post MainPost { get; set; }
    }
}
