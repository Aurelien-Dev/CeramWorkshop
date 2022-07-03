using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Client.Utils
{
    public abstract class PageLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

    }
}
