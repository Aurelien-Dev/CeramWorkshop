using Client.Services.Authentication;
using Client.Utils;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Utils.Exception;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductImageAddDialog : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;
        [Inject] private SessionInfo Session { get; set; } = default!;

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();

        private bool _clearing = false;
        private static string _defaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string _dragClass = _defaultDragClass;

        public ImageInstruction ImageInstruction { get; set; } = new();

        private bool _loading = false;

        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            try
            {
                _loading = true;
                string filePathLoaded = await LoadFileFromInputFile.LoadFileInput(e, Session.WorkshopFolderName);
                filePathLoaded = filePathLoaded.Replace(@"\", "/");

                ImageInstruction.Url = filePathLoaded;
                _loading = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error uploading file for AtelierCremazie");
                throw new UploadFileException("Error uploading file for AtelierCremazie", ex);
            }
        }

        private void OnValidSubmit()
        {
            if (string.IsNullOrEmpty(ImageInstruction.Url)) return;

            ProductDetail.ImageInstructions.Add(ImageInstruction);
            ProductWorker.ProductRepository.Update(ProductDetail);
            ProductWorker.Completed(ComponentDisposed);

            StateHasChanged();
            MudDialog.Close(DialogResult.Ok(true));
        }

        private void SetDragClass()
        {
            _dragClass = $"{_defaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            _dragClass = _defaultDragClass;
        }

        private void Cancel()
        {
            LoadFileFromInputFile.RemoveFileInput(ImageInstruction.Url);
            ImageInstruction = default!;
            MudDialog.Cancel();
        }
    }
}