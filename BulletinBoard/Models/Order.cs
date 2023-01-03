using BulletinBoard.Data.Enums;

namespace BulletinBoard.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Warehouse { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public OrderType? Type { get; set; }

        public OrderStatus? Status { get; set; }

    }
}
