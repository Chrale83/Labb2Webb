using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using Presentation.Interfaces;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CustomerOrderDetails
    {
        [Inject]
        public IOrderService? OrderService { get; set; }

        [Inject]
        public SharedState? State { get; set; }
        public List<OrderForCustomerDTO>? CustomerOrders { get; set; } = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                int customerId = (int)State.SelectedCustomer;
                CustomerOrders = await OrderService.GetOrdersForCustomer(customerId);
            }
            StateHasChanged();
        }
    }
}
