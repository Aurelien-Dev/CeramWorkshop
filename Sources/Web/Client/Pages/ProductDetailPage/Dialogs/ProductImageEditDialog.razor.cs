using Client.Utils;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductImageEditDialog : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ImageInstruction ImageInstruction { get; set; } = new();

        private string OldComment { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                OldComment = ImageInstruction.Comment;
            }
        }

        private void OnValidSubmit()
        {
            ProductWorker.ProductRepository.Update(ProductDetail);
            ProductWorker.Completed();

            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

        private void Cancel()
        {
            ImageInstruction.Comment = OldComment;
            MudDialog.Cancel();
        }
    }
}