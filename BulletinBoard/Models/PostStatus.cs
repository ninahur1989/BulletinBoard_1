using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models
{
    public class PostStatus
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public PostStatuses Status { get; set; }
        public string StatusName { get; set; }
    }
}
