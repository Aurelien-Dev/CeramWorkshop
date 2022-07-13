using Client.Services.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Utils
{
    public abstract class CustomComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public SessionInfo CurrentSession { get; set; } = default!;

        public DialogOptions CommonOptionDialog { get; set; }

        public CustomComponentBase()
        {
            CommonOptionDialog = new DialogOptions
            {
                CloseOnEscapeKey = false,
                DisableBackdropClick = true
            };
        }
    }
}