using Microsoft.AspNetCore.Components;
using Presentation.Interfaces;
using Presentation.Models;
using Presentation.States;

namespace Presentation.Components.Pages
{
    public partial class CustomerEdit
    {
        [Inject]
        public SharedState? AppState { get; set; }

        [Inject]
        public ICustomerService? CustomerService { get; set; }

        public CustomerEditModel? EditedCustomer { get; set; } = new();

        private string message = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (AppState.UserInfo.UserId is int userId)
                {
                    EditedCustomer = await CustomerService.GetCustomerToEdit(userId);
                    StateHasChanged();
                }
            }
        }

        private async Task EditCustomerSubmit()
        {
            await CustomerService.UpdateCustomer(EditedCustomer);
            await StatusMessage("Ändringar har sparats");
        }

        private async Task UndoCustomerEditSubmit()
        {
            if (AppState.UserInfo.UserId is int userId)
            {
                EditedCustomer = await CustomerService.GetCustomerToEdit(userId);
            }
            await StatusMessage("uppgifter har återställts");
            StateHasChanged();
        }

        private async Task StatusMessage(string newMessage)
        {
            message = newMessage;
            StateHasChanged();
            await Task.Delay(3000);
            message = string.Empty;
            StateHasChanged();
        }
    }
}
