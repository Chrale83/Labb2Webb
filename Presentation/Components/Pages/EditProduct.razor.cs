using Microsoft.AspNetCore.Components;
using Presentation.DTOs;

namespace Presentation.Components.Pages
{
    public partial class EditProduct
    {
        [SupplyParameterFromForm]
        public ProductFrontDto? EditingProduct { get; set; } =
            new ProductFrontDto
            {
                Name = "TEST",
                Description = "hdjkshdksjdhsjkdhskjdhsdjkhsdkjshdkjshdkjshdkjsd",
            };

        public List<CategoryDtoApi> Categories { get; set; } =
            new()
            {
                new CategoryDtoApi { Id = 1, Name = "grafik" },
                new CategoryDtoApi { Id = 2, Name = "skärm" },
            };
    }
}
