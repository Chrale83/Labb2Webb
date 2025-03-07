using Microsoft.AspNetCore.Components;
using Presentation.DTOs;

namespace Presentation.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public HttpClient? Http { get; set; }

        [SupplyParameterFromForm]
        public ProductDtoApi Product { get; set; }
        public CategoryDtoApi Category { get; set; }

        protected override void OnInitialized()
        {
            Product ??= new();
            Category ??= new();
        }

        private async Task OnSubmit() { }
    }
}
