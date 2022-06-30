using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.MaterialDetailPage.Dialogs
{
    public partial class AddMaterialDialog
    {
        [Inject] private IProductWork productWorker { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Material Material { get; set; } = new();


        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();

        private async Task OnValidSubmit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                await productWorker.MaterialRepository.Add(Material);
                productWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(Material));
            }
        }


        void Cancel()
        {
            productWorker.Rollback();
            MudDialog.Cancel();
        }
    }
}