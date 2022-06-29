using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Utils.Exception;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductImageEdit
    {
        [Inject] private IProductWork productWorker { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ImageInstruction ImageInstruction { get; set; } = new();

        MudForm form;



        private async Task OnValidSubmit()
        {
            if (ImageInstruction != null && !string.IsNullOrEmpty(ImageInstruction.Url))
            {
                productWorker.ProductRepository.Update(ProductDetail);
                productWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(true));
            }
        }


        void Cancel()
        {
            LoadFileFromInputFile.RemoveFileInput(ImageInstruction.Url);
            ImageInstruction = null;
            MudDialog.Cancel();
        }
    }
}
