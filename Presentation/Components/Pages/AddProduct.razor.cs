using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Extensions;
using Presentation.Interfaces;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public IProductService? ProductService { get; set; }

        [Inject]
        public AppState? AppState { get; set; }

        [SupplyParameterFromForm]
        public ProductDTO? Product { get; set; }

        public List<CategoryDTO> Categories { get; set; } = new();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductSaved = false;

        protected override async Task OnInitializedAsync()
        {
            Product ??= new() { CategoryId = 1 };
            Categories = AppState.Categories;
            //TODO
            //
            // Fixa så om appstate.categories är null hämta igen
        }

        private void HandleInvalidSubmit()
        {
            statusClass = "alert-danger";
            message = "Check your forms";
        }

        private async Task CreateProduct()
        {
            var response = await ProductService.CreateProductAsync(Product);

            if (response)
            {
                statusClass = "alert-success";
                message = "Produkten är tillagd i listan";
                isProductSaved = true;
            }
            else
            {
                message = "Nåt gick fel";
                statusClass = "alert-danger";
            }
        }
    }
}
