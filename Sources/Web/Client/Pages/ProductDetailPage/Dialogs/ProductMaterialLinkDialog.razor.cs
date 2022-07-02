using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductMaterialLinkDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Material MaterialDetail { get; set; } = new();
        [Parameter] public ProductMaterial ProductMaterialDetail { get; set; } = new();

        MudForm form = new();

        protected override Task OnInitializedAsync()
        {
            if (MaterialDetail.Cost.HasValue)
                ProductMaterialDetail.Cost = MaterialDetail.Cost.Value;

            return base.OnInitializedAsync();
        }

        private async Task OnValidSubmit()
        {
            StateHasChanged();
            await form.Validate();

            if (form.IsValid)
            {
                ProductMaterialDetail.Material = MaterialDetail;
                MudDialog.Close(DialogResult.Ok(ProductMaterialDetail));
            }
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
