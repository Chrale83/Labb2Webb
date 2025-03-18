using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;

        [MaxLength(8)]
        public string StreetNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
    }
}
