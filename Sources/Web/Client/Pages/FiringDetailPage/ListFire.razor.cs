using Client.Pages.FiringDetailPage.Dialogs;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.FiringDetailPage
{
    [Authorize]
    public partial class ListFire : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;

        [Parameter] public string Title { get; set; }

        private ICollection<Firing> Firings { get; set; } = new List<Firing>();

        protected override async Task OnInitializedAsync()
        {
            await LoadDatas();
        }

        private async Task LoadDatas()
        {
            Firings = await ProductWorker.FiringRepository.GetAll(ComponentDisposed);
        }

        private async Task DeleteFiringDialog(Firing firing)
        {
            bool? result = await DialogService.ShowMessageBox("Suppression cette cuisson", "Voulez-vous supprimer cette cuisson ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            await ProductWorker.FiringRepository.Delete(firing);

            Firings.Remove(firing);
            StateHasChanged();
        }

        private async Task AddFiringDialog()
        {
            var parameters = new DialogParameters { ["InsertMode"] = true };

            var dialog = await DialogService.ShowAsync<FiringDialog>("Ajouter une nouvelle Cuisson", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Canceled) return;

            Firings.Add((Firing)result.Data);
        }

        private async Task EditFiringDialog(Firing firing)
        {
            var parameters = new DialogParameters { ["FiringDetail"] = firing };

            var dialog = await DialogService.ShowAsync<FiringDialog>("Modifier une cuisson", parameters, this.CommonOptionDialog);
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