using System.ComponentModel.DataAnnotations;

namespace Presentation.DTOs
{
    public class ProductDtoApi
    {
        [Required(ErrorMessage = "Required to enter")]
        [MaxLength(50, ErrorMessage = "Max 50 letters")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
