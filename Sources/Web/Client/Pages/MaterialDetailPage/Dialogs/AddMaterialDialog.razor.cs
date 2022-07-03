using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.MaterialDetailPage.Dialogs
{
    public partial class AddMaterialDialog
    {
        [Inject] private IProductWork productWorker { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Material MaterialDetail { get; set; } = new();
        [Parameter] public bool? InsertMode { get; set; } = new();

        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();

        private async Task OnValidSubmit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                if (InsertMode.HasValue && InsertMode.Value)
                    await productWorker.MaterialRepository.Add(MaterialDetail);
                else
                    productWorker.MaterialRepository.Update(MaterialDetail);

                productWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(MaterialDetail));
            }
        }


        void Cancel()
        {
            productWorker.Rollback();
            StateHasChanged();
            MudDialog.Cancel();
        }
    }
}