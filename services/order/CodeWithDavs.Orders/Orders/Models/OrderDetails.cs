using Orders.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Models
{
    public class OrderDetails
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
    }
}
