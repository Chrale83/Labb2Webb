namespace Presentation.DTOs
{
    public class CustomerLoginResponseDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
