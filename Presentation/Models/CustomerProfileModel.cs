namespace Presentation.Models
{
    public class CustomerProfileModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
        public string FullAdress => $"{StreetName} {StreetNumber} {City}";
    }
}
