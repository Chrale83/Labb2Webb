using Presentation.DTOs;

namespace Presentation.Models
{
    public class ShoppingCartItemModel
    {
        public ProductDTO? Product { get; set; }
        public int Quantity { get; set; }
    }
}
