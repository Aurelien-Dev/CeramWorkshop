using Client.Services;
using Client.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Utils
{
    public abstract class CustomLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public AuthenticationService authenticationServiceSingleton { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;
        [Inject] public SessionInfo CurrentSession { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
    }
}
