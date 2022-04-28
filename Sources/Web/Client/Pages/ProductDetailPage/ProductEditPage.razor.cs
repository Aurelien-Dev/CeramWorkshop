using Client.Utils;
using Common.Helpers.RazorComponent;
using Common.Helpers.RazorComponent.CommonControls;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Utils.Exception;


namespace Client.Pages.ProductDetailPage
{

    public partial class ProductEditPage : PageComponentBase
    {
        [Parameter] public int? Id { get; set; }
        [Inject] private IProductWork productWorker { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        public Product ProductDetail { get; set; } = new();
        public ImageInstruction ImageInstruction { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                await LoadData(Id.Value);
                Mode = PageMode.Edit;
            }
            else
            {
                ProductDetail = new Product();
                Mode = PageMode.New;
            }
        }

        private async Task LoadData(int id)
        {
            ProductDetail = await productWorker.ProductRepository.Get(id);
        }

        public void DeleteImageCmd(int idImage)
        {
            ImageInstruction deleteImage = ProductDetail.ImageInstructions.Where(i => i.Id == idImage).FirstOrDefault();
            if (deleteImage != null)
            {
                ProductDetail.ImageInstructions.Remove(deleteImage);
            }
        }

        public void UndoCmd()
        {
            if (!Id.HasValue)
            {
                NavigationManager.NavigateTo($"/");
                return;
            }
            productWorker.Rollback();
            NavigationManager.NavigateTo($"/Product/{Id}");
        }

        public void AddImageEventHandler(EditContext context)
        {
            ProductDetail.ImageInstructions.Add(ImageInstruction);
            ImageInstruction = new();
        }

        public void SaveProductEventHandler(EditContext context)
        {
            bool isValid = context.Validate();
            if (isValid)
            {
                if (Mode == PageMode.New)
                {
                    productWorker.ProductRepository.Add(ProductDetail);
                    int result = productWorker.Completed();
                }

                if (Mode == PageMode.Edit)
                {
                    productWorker.ProductRepository.Update(ProductDetail);
                    int result = productWorker.Completed();
                }
                if (Id.HasValue) NavigationManager.NavigateTo($"/Product/{Id}");
                else NavigationManager.NavigateTo("/");
            }
        }

        private async Task UploadFile(InputFileChangeEventArgs e)
        {
            try
            {
                string filePathLoaded = await LoadFileFromInputFile.LoadFileInput(e, "AtelierCremazie");
                ImageInstruction.Url = filePathLoaded;
                ImageInstruction.ThumbUrl = filePathLoaded;
                ImageInstruction.MediumUrl = filePathLoaded;
            }
            catch (Exception ex)
            {
                throw new UploadFileException("Error uploading file for AtelierCremazie", ex);
            }
        }
    }
}