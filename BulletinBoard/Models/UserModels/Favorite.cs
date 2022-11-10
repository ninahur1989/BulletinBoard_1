using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.UserModels
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("PostId")]
        public int PostId { get; set; }
    }
}
