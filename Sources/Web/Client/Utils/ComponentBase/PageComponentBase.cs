using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Utils
{
    public enum PageMode { New, Edit, Show }

    public abstract class PageComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IModalService ModalService { get; set; } = default!;

        public PageMode Mode { get; set; }


        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            JSRuntime.InvokeVoidAsync("FeatherInit");
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task OpenModal(string idModal)
        {
            await JSRuntime.InvokeAsync<string>("OpenModal", new string[] { idModal });
        }

        public async Task JSCloseModal(string idModal)
        {
            await JSRuntime.InvokeAsync<string>("CloseModal", new string[] { idModal });
        }

        public async Task InvokeStateHasChanged()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}