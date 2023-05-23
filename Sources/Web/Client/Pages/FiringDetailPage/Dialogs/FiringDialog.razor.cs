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
        private Firing OriginalFiringDetail { get; set; } = new();
        [Parameter] public bool? InsertMode { get; set; } = new();

        private MudForm _form = new();
        private bool _success;
        private string[] _errors = Array.Empty<string>();

        protected override void OnParametersSet()
        {
            OriginalFiringDetail = FiringDetail.Clone();
        }

        private async Task OnValidSubmit()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                if (InsertMode.HasValue && InsertMode.Value)
                    await ProductWorker.FiringRepository.Add(FiringDetail, ComponentDisposed);
                else
                    await ProductWorker.FiringRepository.Update(FiringDetail);
                ComponentDisposed.ThrowIfCancellationRequested();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(FiringDetail));
            }
        }

        private void Cancel()
        {
            StateHasChanged();
            MudDialog.Cancel();
            
            FiringDetail.Name = OriginalFiringDetail.Name;
            FiringDetail.Duration = OriginalFiringDetail.Duration;
            FiringDetail.CostKwH = OriginalFiringDetail.CostKwH;
            FiringDetail.TotalKwH = OriginalFiringDetail.TotalKwH;

        }
    }
}