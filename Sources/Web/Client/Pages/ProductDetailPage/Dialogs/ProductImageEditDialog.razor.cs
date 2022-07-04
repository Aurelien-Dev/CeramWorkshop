using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductImageEditDialog
    {
        [Inject] private IProductWorker productWorker { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ImageInstruction ImageInstruction { get; set; } = new();

        private void OnValidSubmit()
        {
            if (ImageInstruction != null && !string.IsNullOrEmpty(ImageInstruction.Url))
            {
                productWorker.ProductRepository.Update(ProductDetail);
                productWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private void Cancel()
        {
            LoadFileFromInputFile.RemoveFileInput(ImageInstruction.Url);
            ImageInstruction = default!;
            MudDialog.Cancel();
        }
    }
}