using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Presentation.Components.Dialogs
{
    public partial class ConfirmDeleteProductDialog
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string ProductName { get; set; } = string.Empty;

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();
    }
}
