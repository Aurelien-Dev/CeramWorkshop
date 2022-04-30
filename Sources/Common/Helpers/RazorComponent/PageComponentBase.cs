using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Common.Helpers.RazorComponent
{
    public enum PageMode { New, Edit, Show }

    public abstract class PageComponentBase : ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        public PageMode Mode { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            JSRuntime.InvokeVoidAsync("FeatherInit");


            return base.OnAfterRenderAsync(firstRender);
        }


        public void OpenModal(string idModal, params object?[] args)
        {
            JSRuntime.InvokeAsync<string>("OpenModal", new string[] { idModal });
        }
    }
}
