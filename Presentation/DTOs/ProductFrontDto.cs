using System.ComponentModel.DataAnnotations;

namespace Presentation.DTOs
{
    public class ProductFrontDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required to enter")]
        [MaxLength(50, ErrorMessage = "Max 50 letters")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required to enter")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required to enter")]
        [Range(1, int.MaxValue, ErrorMessage = "Price need to be higher than zero")]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
