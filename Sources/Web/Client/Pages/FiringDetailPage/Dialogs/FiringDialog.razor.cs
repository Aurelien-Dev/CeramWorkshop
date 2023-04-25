using Client.Utils;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.FiringDetailPage.Dialogs
{
    public partial class FiringDialog : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Firing FiringDetail { get; set; } = new();
        [Parameter] public bool? InsertMode { get; set; } = new();

        private MudForm _form = new();
        private bool _success;
        private string[] _errors = Array.Empty<string>();


        private async Task OnValidSubmit()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                if (InsertMode.HasValue && InsertMode.Value)
                    await ProductWorker.FiringRepository.Add(FiringDetail);
                else
                    ProductWorker.FiringRepository.Update(FiringDetail);

                await ProductWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(FiringDetail));
            }
        }

        private void Cancel()
        {
            ProductWorker.Rollback();
            StateHasChanged();
            MudDialog.Cancel();
        }
    }
}