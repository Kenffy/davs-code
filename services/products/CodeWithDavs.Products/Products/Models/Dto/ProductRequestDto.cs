namespace Products.Models.Dto
{
    public class ProductRequestDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string CategoryId { get; set; }
        public string Github { get; set; }
        public string Youtube { get; set; }
    }
}
