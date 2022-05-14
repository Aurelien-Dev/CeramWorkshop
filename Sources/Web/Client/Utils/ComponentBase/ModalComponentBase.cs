using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Utils
{

    public abstract class ModalComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IModalService ModalService { get; set; } = default!;

        [Parameter] public ModalInstance Instance { get; set; }
        [Parameter] public string IdModal { get; set; }

        

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                ((ModalService)ModalService).ShowModal(IdModal);

            return base.OnAfterRenderAsync(firstRender);
        }

        public void Close(bool confirm)
        {
            ((ModalService)ModalService).CloseModal(IdModal);
            Instance.ModalClosed(confirm);
            StateHasChanged();
        }
    }
}