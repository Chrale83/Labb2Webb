using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class NewCustomer
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email måste fyllas i")]
        public string Email { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Du måste bekräfta email adressen")]
        [Compare("Email", ErrorMessage = "Emailen måste matcha")]
        public string SecondEmail { get; set; } = string.Empty;

        [PasswordPropertyText]
        [Required(ErrorMessage = "Lösenord krävs")]
        public string Password { get; set; } = string.Empty;

        [PasswordPropertyText]
        [Required(ErrorMessage = "Du måste bekräfta lösenordet")]
        [Compare("Password", ErrorMessage = "Lösenorden måste matcha")]
        public string SecondPassword { get; set; } = string.Empty;

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
