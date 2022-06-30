using Client.Pages.MaterialDetailPage.Dialogs;
using Client.Utils;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.MaterialDetailPage
{
    public partial class ListMaterialPage : PageComponentBase
    {
        [Inject] private IMaterialRepository MaterialRepository { get; set; } = default!;

        public ICollection<Material> Materials { get; set; } = new List<Material>();

        protected override async Task OnInitializedAsync()
        {
            Materials = await MaterialRepository.GetAll();
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