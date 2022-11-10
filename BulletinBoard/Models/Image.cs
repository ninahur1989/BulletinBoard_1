using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
