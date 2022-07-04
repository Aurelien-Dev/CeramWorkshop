using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Utils.Exception;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductImageAddDialog
    {
        [Inject] private IProductWorker productWorker { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();

        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;

        public ImageInstruction ImageInstruction { get; set; } = new();


        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            try
            {
                string filePathLoaded = await LoadFileFromInputFile.LoadFileInput(e, "AtelierCremazie");
                filePathLoaded = filePathLoaded.Replace(@"\", "/");

                ImageInstruction.Url = filePathLoaded;
                ImageInstruction.ThumbUrl = filePathLoaded;
                ImageInstruction.MediumUrl = filePathLoaded;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw new UploadFileException("Error uploading file for AtelierCremazie", ex);
            }
        }

        private void OnValidSubmit()
        {
            if (ImageInstruction != null && !string.IsNullOrEmpty(ImageInstruction.Url))
            {
                ProductDetail.ImageInstructions.Add(ImageInstruction);
                productWorker.ProductRepository.Update(ProductDetail);
                productWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }

        private void Cancel()
        {
            LoadFileFromInputFile.RemoveFileInput(ImageInstruction.Url);
            ImageInstruction = default!;
            MudDialog.Cancel();
        }
    }
}