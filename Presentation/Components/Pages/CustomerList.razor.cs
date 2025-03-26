using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Interfaces;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CustomerList
    {
        [Inject]
        public ICustomerService? CustomerService { get; set; }

        [Inject]
        public AppState? AppState { get; set; }

        public List<CustomerProfileModel>? CustomerProfiles { get; set; } = new();
        public CustomerProfileModel? SelectedCustomer { get; set; } = new();

        private MudTable<CustomerProfileModel>? mudTable;
        private string message = string.Empty;
        private CancellationTokenSource _cts;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CustomerProfiles = await CustomerService.GetAllCustomers();

                StateHasChanged();
            }
        }

        private async Task SearchForCustomersName(ChangeEventArgs input)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            try
            {
                await Task.Delay(600, _cts.Token);
                var customerQuery = input.Value?.ToString();

                var listHaveCustomers = CustomerProfiles.Any();
                CustomerProfiles = await CustomerService.GetCustomerFromSearch(
                    customerQuery,
                    listHaveCustomers
                );
            }
            catch (TaskCanceledException)
            {
                //Sökningen avbröts eller skrev för snabbt
            }
            StateHasChanged();
        }
    }
}
