using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Presentation.DTOs;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        private IEnumerable<ProductFrontDto> Products { get; set; } = new List<ProductFrontDto>();
        public List<CategoryDtoApi> Categories { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Categories = await Http.GetFromJsonAsync<List<CategoryDtoApi>>("/api/categories");
        }

        //try
        //{
        //    Products =
        //        await Http.GetFromJsonAsync<List<ProductDtoApi>>("/api/products") ?? new();
        //}
        //catch (Exception ex)
        //{
        //    Console.Error.WriteLine($"Fel vid hämtning av produktlista: {ex.Message}");
        //}


        private async Task<GridDataProviderResult<ProductFrontDto>> ProductsDataGridProvider(
            GridDataProviderRequest<ProductFrontDto> request
        )
        {
            Products = await Http.GetFromJsonAsync<List<ProductFrontDto>>("/api/products") ?? new();
            return await Task.FromResult(request.ApplyTo(Products ?? new List<ProductFrontDto>()));
        }
    }
}
