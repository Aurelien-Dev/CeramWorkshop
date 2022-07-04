using Client.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Utils
{
    public abstract class CustomLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        public bool IsAuthenticate { get; set; } = AuthenticationServiceSingleton.LoginInfo.IsAuthenticate;
    }
}
