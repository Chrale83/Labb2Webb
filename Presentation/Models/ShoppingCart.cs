namespace Presentation.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItemModel> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(item => item.Product.Price * item.Quantity);
    }
}
