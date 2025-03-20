using Microsoft.AspNetCore.Components;

namespace Presentation.Components.Pages
{
    public partial class CustomerEdit
    {
        [Inject]
        public IHttpClientFactory httpClientFactory { get; set; }
        public HttpClient httpClient { get; set; }
        public CustomerEdit EditedCustomer { get; set; } = new();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
