using Client.Pages.MaterialDetailPage.Dialogs;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.MaterialDetailPage
{
    public partial class ListMaterial : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;

        [Parameter] public string Title { get; set; }
        [Parameter] public MaterialType MaterialType { get; set; }

        private ICollection<Material> Materials { get; set; } = new List<Material>();

        protected override async Task OnInitializedAsync()
        {
            await LoadDatas();
        }


        private async Task LoadDatas()
        {
            Materials = await ProductWorker.MaterialRepository.GetAll(MaterialType, ComponentDisposed);
        }

        private async Task DeleteMat(Material material)
        {
            bool? result = await DialogService.ShowMessageBox("Suppression cette matière", "Voulez-vous cette matière ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            ProductWorker.MaterialRepository.Delete(material);
            await ProductWorker.Completed(ComponentDisposed);

            Materials.Remove(material);
            StateHasChanged();
        }

        private async Task AddMaterialDialog()
        {
            var parameters = new DialogParameters { ["InsertMode"] = true, ["MaterialType"] = MaterialType };

            var dialog = await DialogService.ShowAsync<MaterialDialog>("Ajouter une nouvelle matière", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Canceled) return;

            Materials.Add((Material)result.Data);
        }

        private async Task EditMat(Material material)
        {
            var parameters = new DialogParameters { ["MaterialDetail"] = material, ["MaterialType"] = material.Type };

            var dialog = await DialogService.ShowAsync<MaterialDialog>("Ajouter une nouvelle matière", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Canceled)
            {
                await LoadDatas();
                return;
            }

            StateHasChanged();
        }

    }
}