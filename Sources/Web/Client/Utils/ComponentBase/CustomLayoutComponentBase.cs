using Client.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Client.Utils.ComponentBase
{
    public abstract class CustomLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public SessionInfo CurrentSession { get; set; } = new();
        [Inject] public IStringLocalizer<Translations> Localizer { get; set; }

    }
}