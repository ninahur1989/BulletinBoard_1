using BulletinBoard.Data.Enums;
using BulletinBoard.Models.AttributeModels;
using BulletinBoard.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Titile { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsEnable { get; set; }
        public string Location { get; set; }
        public int CountInFavorites { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public int Attribute_PostId { get; set; }
        [ForeignKey(nameof(Attribute_PostId))]
        public virtual Attribute_Post MainAttribute_Post { get; set; }

        public int PostStatusId { get; set; }
        [ForeignKey(nameof(PostStatusId))]
        public virtual PostStatus Status { get; set; }

        public int AttributeCategoryId { get; set; }
        [ForeignKey(nameof(AttributeCategoryId))]
        public virtual AttributeCategory Category { get; set; }

        public virtual List<Image> Images { get; set; }
    }
}
