using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.DTOs;
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
        public IOrderService? OrderService { get; set; }

        [Inject]
        public SharedState? AppState { get; set; }
        public List<OrderForCustomerDTO>? SelectedOrders { get; set; } = new();

        public List<CustomerProfileModel>? CustomerProfiles { get; set; } = new();
        public CustomerProfileModel? SelectedCustomer { get; set; } = new();

        private MudTable<CustomerProfileModel>? mudTable;
        private string message = string.Empty;
        private CancellationTokenSource _cts;
        private bool isSHowingOrders = false;

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

        private void ShowOrders(CustomerProfileModel selectedCustomer)
        {
            AppState.SelectedCustomer = selectedCustomer.Id;
            Navigation.NavigateTo("/customerorderdetails");
        }

        private async Task DeleteCustomer(CustomerProfileModel SelectedCustomer)
        {
            await CustomerService.DeleteCustomer(SelectedCustomer.Id);
            CustomerProfiles = await CustomerService.GetAllCustomers();
            StateHasChanged();
        }
    }
}
