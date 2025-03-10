using Microsoft.AspNetCore.Components;
using Presentation.DTOs;

namespace Presentation.Components.Pages
{
    public partial class ManageProducts
    {
        [Inject]
        public HttpClient Http { get; set; }

        public List<ProductDtoApi> Products { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products =
                    await Http.GetFromJsonAsync<List<ProductDtoApi>>("/api/products") ?? new();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Fel vid hämtning av produktlista{ex.Message}");
                Products = new List<ProductDtoApi>();
            }
        }
    }
}
