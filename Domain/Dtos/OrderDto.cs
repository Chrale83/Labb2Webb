namespace Presentation.DTOs
{
    public class OrderDTO
    {
        public List<ProductOrderDTO>? ProductOrders { get; set; } = new();
        public DateTime Ordertime { get; set; } = DateTime.Now;
        public decimal TotalCost { get; set; }
    }
}
