namespace Presentation.DTOs
{
    public class OrderProductForCustomerDTO
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public string? CategoryName { get; set; }

        public int Quantity { get; set; }
    }
}
