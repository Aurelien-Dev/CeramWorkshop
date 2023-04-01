using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Client.Pages.MaterialDetailPage.Dialogs
{
    public partial class MaterialDialog
    {
        [Inject] private IProductWorker productWorker { get; set; } = default!;
        [Inject] public IStringLocalizer<Translations> Localizer { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Material MaterialDetail { get; set; } = new();
        [Parameter] public MaterialType? MaterialType { get; set; } = new();
        [Parameter] public bool? InsertMode { get; set; } = new();

        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();

        protected override void OnAfterRender(bool firstRender)
        {
            if (MaterialType.HasValue)
                MaterialDetail.Type = MaterialType.Value;
        }

        private async Task OnValidSubmit(bool updateAllProductsMat = false)
        {
            await form.Validate();

            if (form.IsValid)
            {
                if (InsertMode.HasValue && InsertMode.Value)
                    await productWorker.MaterialRepository.Add(MaterialDetail);
                else
                {
                    productWorker.MaterialRepository.Update(MaterialDetail);

                    if (updateAllProductsMat)
                        productWorker.MaterialRepository.UpdateAllMaterialCost(MaterialDetail.Id);
                }

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