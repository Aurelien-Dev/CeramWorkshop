using Client.Pages.MaterialDetailPage.Dialogs;
using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.MaterialDetailPage
{
    public partial class ListMaterialPage : PageComponentBase
    {
        [Inject] private IProductWork worker { get; set; } = default!;

        public ICollection<Material> Materials { get; set; } = new List<Material>();

        protected override async Task OnInitializedAsync()
        {
            Materials = await worker.MaterialRepository.GetAll();
        }
        
        private async Task DeleteMat(Material material)
        {
            bool? result = await DialogService.ShowMessageBox("Suppression cette matière", "Voulez-vous cette matière ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            worker.MaterialRepository.Delete(material);
            worker.Completed();

            Materials.Remove(material);
            StateHasChanged();
        }

        private async Task AddMaterialDialog()
        {
            var dialog = DialogService.Show<AddMaterialDialog>("Ajouter une nouvelle matière", this.commonOptionDialog);
            var result = await dialog.Result;

            if (result.Cancelled) return;

            Materials.Add((Material)result.Data);
        }
    }
}