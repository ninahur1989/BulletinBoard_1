using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class AnimalAttribute : IAttribute
    {
        private Categories _categorie = Categories.Animal;

        public int Id { get; set; }
        public int Age { get; set; }
        public Categories Categorie { get { return _categorie; } }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}
