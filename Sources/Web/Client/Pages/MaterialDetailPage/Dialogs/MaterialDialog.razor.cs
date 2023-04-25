using Client.Utils;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.MaterialDetailPage.Dialogs
{
    public partial class MaterialDialog : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; }

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Material MaterialDetail { get; set; } = new();
        [Parameter] public MaterialType? MaterialType { get; set; } = new();
        [Parameter] public bool? InsertMode { get; set; } = new();

        private MudForm _form = new();
        private bool _success;
        private string[] _errors = Array.Empty<string>();

        protected override void OnAfterRender(bool firstRender)
        {
            if (MaterialType.HasValue)
                MaterialDetail.Type = MaterialType.Value;
        }

        private async Task OnValidSubmit(bool updateAllProductsMat = false)
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                if (InsertMode.HasValue && InsertMode.Value)
                    await ProductWorker.MaterialRepository.Add(MaterialDetail);
                else
                {
                    ProductWorker.MaterialRepository.Update(MaterialDetail);

                    if (updateAllProductsMat)
                        await ProductWorker.MaterialRepository.UpdateAllMaterialCost(MaterialDetail.Id);
                }

                await ProductWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(MaterialDetail));
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