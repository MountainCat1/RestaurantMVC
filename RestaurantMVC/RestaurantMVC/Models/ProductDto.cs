using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.Models
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 4)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
