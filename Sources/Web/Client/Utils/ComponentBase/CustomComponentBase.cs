using Client.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using MudBlazor;
using System.Globalization;

namespace Client.Utils
{
    public abstract class CustomComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public SessionInfo CurrentSession { get; set; } = default!;
        [Inject] public IStringLocalizer<Translations> Localizer { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public ILogger Logger { get; set; } = default!;

        public CultureInfo CurrentCultur { get => CultureInfo.CreateSpecificCulture(CurrentSession.Workshop.Culture); }

        public DialogOptions CommonOptionDialog { get; set; } = new DialogOptions
        {
            CloseOnEscapeKey = false,
            DisableBackdropClick = true
        };
    }
}