using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }

        [MaxLength(8)]
        public string StreetNumber { get; set; }
        public string City { get; set; }

        public ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
    }
}
