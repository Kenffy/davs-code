using Orders.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        [NotMapped]
        public AddressDto Address { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
