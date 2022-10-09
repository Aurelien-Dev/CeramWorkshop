using Client.Pages.FiringDetailPage.Dialogs;
using Client.Utils;
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
        [Inject] private IProductWorker worker { get; set; } = default!;

        [Parameter] public string Title { get; set; }

        public ICollection<Firing> Firings { get; set; } = new List<Firing>();

        protected override async Task OnInitializedAsync()
        {
            await LoadDatas();
        }

        private async Task LoadDatas()
        {
            Firings = await worker.FiringRepository.GetAll();
        }

        private async Task DeleteFiringDialog(Firing firing)
        {
            bool? result = await DialogService.ShowMessageBox("Suppression cette cuisson", "Voulez-vous supprimer cette cuisson ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            worker.FiringRepository.Delete(firing);
            worker.Completed();

            Firings.Remove(firing);
            StateHasChanged();
        }

        private async Task AddFiringDialog()
        {
            var parameters = new DialogParameters { ["InsertMode"] = true };

            var dialog = DialogService.Show<FiringDialog>("Ajouter une nouvelle Cuisson", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Cancelled) return;

            Firings.Add((Firing)result.Data);
        }

        private async Task EditFiringDialog(Firing firing)
        {
            var parameters = new DialogParameters { ["FiringlDetail"] = firing };

            var dialog = DialogService.Show<FiringDialog>("Modifier une cuisson", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Cancelled)
            {
                await LoadDatas();
                return;
            }
            StateHasChanged();
        }
    }
}
