using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Common.Helpers.RazorComponent
{
    public abstract class PageLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

        public void OpenModal(string idModal)
        {
            JSRuntime.InvokeAsync<string>("OpenModal", new string[] { idModal });
        }
    }
}
