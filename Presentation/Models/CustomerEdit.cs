using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CustomerEdit
    {
        [Required(ErrorMessage = "Förnamn måste fyllas i")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Efternamn måste fyllas i")]
        public string LastName { get; set; } = string.Empty;

        [RegularExpression("([0-9]+)", ErrorMessage = "fyll i ett nummer")]
        [Required(ErrorMessage = "Telefon nummer måste fyllas i")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gatunamn måste fyllas i")]
        public string StreetName { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "mellan 1-1000")]
        [Required(ErrorMessage = "Gatunummer  måste fyllas i")]
        public string StreetNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stad måste fyllas i")]
        public string City { get; set; } = string.Empty;
    }
}
