using System.Diagnostics.Eventing.Reader;
using Presentation.DTOs;
using Presentation.Models;

namespace Presentation.Services
{
    public class ShoppingCartService
    {
        public List<ShoppingCartItemModel> Items { get; set; } = new();
        public Action? OnChange;

        public void AddToCart(ProductDTO product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new ShoppingCartItemModel { Product = product, Quantity = quantity });
            }

            NotifyStateChanged();
        }

        public void RemoveFromCart(int productId)
        {
            var itemToRemove = Items.FirstOrDefault(p => p.Product.Id == productId);
            Items.Remove(itemToRemove);
            NotifyStateChanged();
        }

        public void ClearCart() => Items.Clear();

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
