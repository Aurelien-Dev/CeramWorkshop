using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Utils
{
    public abstract class PageComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        public DialogOptions CommonOptionDialog { get; set; } = new DialogOptions { CloseOnEscapeKey = false, DisableBackdropClick = true };
    }
}