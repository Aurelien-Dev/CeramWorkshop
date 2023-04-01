using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductImageEditDialog
    {
        [Inject] private IProductWorker productWorker { get; set; } = default!;
        [Inject] public IStringLocalizer<Translations> Localizer { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
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
            ImageInstruction.Comment = OldComment;
            MudDialog.Cancel();
        }
    }
}