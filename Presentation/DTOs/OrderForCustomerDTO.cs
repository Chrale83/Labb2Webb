namespace Presentation.DTOs
{
    public class OrderForCustomerDTO
    {
        public int OrderId { get; set; }

        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderProductForCustomerDTO> Products { get; set; } =
            new List<OrderProductForCustomerDTO>();
    }
}
