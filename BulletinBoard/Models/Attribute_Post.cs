using BulletinBoard.Models.AttributeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models
{
    public class Attribute_Post
    {
        [ForeignKey("PostId")]
        public int Id { get; set; }
        public virtual Post MainPost { get; set; }

        //public int? AnimalAttributeId { get; set; }
        public virtual AnimalAttribute? AnimalAttribute { get; set; }

        //public int? CarAttributeId { get; set; }
        public virtual CarAttribute? CarAttribute { get; set; }
    }
}
