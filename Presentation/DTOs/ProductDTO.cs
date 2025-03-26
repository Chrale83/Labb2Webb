using System.ComponentModel.DataAnnotations;

namespace Presentation.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Måste fylla i namn på produkten")]
        [MaxLength(50, ErrorMessage = "Max 50 bokstäver")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Måste fylla i en beskrivning")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Måste fylla i pris")]
        [Range(1, int.MaxValue, ErrorMessage = "Priset nåste vara mer än 0")]
        public decimal Price { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
