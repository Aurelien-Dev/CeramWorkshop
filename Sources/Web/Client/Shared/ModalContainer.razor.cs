using Client.Utils;

namespace Client.Shared
{
    public partial class ModalContainer : PageComponentBase
    {
        public ICollection<ModalInstance> renderFragments { get; set; } = new List<ModalInstance>();


        protected override Task OnInitializedAsync()
        {
            ((ModalService)ModalService).ModalAdded += async (instance) =>
            {
                //Si la modal existe déjà on l'ouvre
                if (renderFragments.Any(m => m.Id == instance.Id))
                {
                    await OpenModal(instance.Id);
                    return;
                }

                renderFragments.Add(instance);
                await InvokeStateHasChanged();
            };

            ((ModalService)ModalService).ShowModal = async (id) =>
            {
                await Task.Delay(200);
                await OpenModal(id);
            };

            ((ModalService)ModalService).CloseModal = async (id) =>
            {
                ModalInstance modal = renderFragments.FirstOrDefault(m => m.Id == id);

                if (modal == null) return;

                await JSCloseModal(modal.Id);
                await Task.Delay(500); // delay d'attente que bootstrap ferme la modal

                if (modal != null)
                    renderFragments.Remove(modal);
                await InvokeStateHasChanged();
            };

            return base.OnInitializedAsync();
        }
    }
}