

namespace Orders.Models.Dto
{
    public class CartDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public object Detail { get; set; }
        public string CartId { get; set; }
    }
}
