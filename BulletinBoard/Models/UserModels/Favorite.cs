using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.UserModels
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("PostId")]
        public int PostId { get; set; }
    }
}
