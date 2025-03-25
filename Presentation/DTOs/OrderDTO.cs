namespace Presentation.DTOs
{
    public class OrderDTO
    {
        public List<ProductOrderDTO>? ProductOrders { get; set; }
        public DateTime Ordertime { get; set; } = DateTime.Now;
    }
}
