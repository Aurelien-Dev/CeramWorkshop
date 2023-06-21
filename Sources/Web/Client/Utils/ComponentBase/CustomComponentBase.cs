using System.Globalization;
using Client.Services;
using Client.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Utils.ComponentBase
{
    public abstract class CustomComponentBase : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        [Inject] public AuthenticationService AuthenticationManager { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        [Inject] public SessionInfo CurrentSession { get; set; } = default!;
        [Inject] public IStringLocalizer<Translations> Localizer { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; } = default!;
        [Inject] public ILogger Logger { get; set; } = default!;
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        protected CultureInfo CurrentCultur => CultureInfo.CreateSpecificCulture(CurrentSession.Workshop.Culture);

        public DialogOptions CommonOptionDialog { get; } = new()
        {
            CloseOnEscapeKey = false,
            DisableBackdropClick = true
        };

        protected CancellationToken ComponentDisposed => (_cancellationTokenSource ??= new()).Token;
        private CancellationTokenSource? _cancellationTokenSource;

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}