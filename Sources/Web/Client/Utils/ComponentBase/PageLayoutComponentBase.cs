using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Utils
{
    public abstract class PageLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IModalService ModalService { get; set; } = default!;

        public void OpenModal(string idModal)
        {
            JSRuntime.InvokeAsync<string>("OpenModal", new string[] { idModal });
        }
    }
}
