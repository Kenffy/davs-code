using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
