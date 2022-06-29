using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Utils
{
    public enum PageMode { New, Edit, Show }

    public abstract class PageComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

        public PageMode Mode { get; set; }


        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task InvokeStateHasChanged()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}