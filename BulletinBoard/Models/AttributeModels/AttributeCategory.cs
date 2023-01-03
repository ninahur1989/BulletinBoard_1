using BulletinBoard.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.AttributeModels
{
    public class AttributeCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public Categories Category { get; set; }
        public string CategoryName { get; set; }
    }
}
