using System.ComponentModel.DataAnnotations;

namespace RetroGameStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0.01, 999999)]
        public double Price { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        // Reference the parent class (1 category -> many products)
        public Category? Category { get; set; }
    }
}
