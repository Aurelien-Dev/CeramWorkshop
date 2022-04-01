using Client.Utils;
using Common.Helpers.RazorComponent.CommonControls;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Utils.Exception;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductInfoPage : ComponentBase
    {
        [Inject] private IProductWork unitOfWork { get; set; }

        [Parameter] public int Id { get; set; }


        public Product ProductDetail { get; set; } = new Product();
        public bool IsReadonly { get; set; } = false;

        List<CarouselCardItem> ProductImages = new List<CarouselCardItem>();


        protected override async Task OnInitializedAsync()
        {
            ProductDetail = await unitOfWork.ProductRepository.Get(Id);

            foreach (var item in ProductDetail.ProductImageInstruction)
            {
                ProductImages.Add(new CarouselCardItem(item.Url, item.Comment));
            }
        }

        private async Task UploadFile(InputFileChangeEventArgs e)
        {
            try
            {
                string filePathLoaded = await LoadFileFromInputFile.LoadFileInput(e, "AtelierCremazie");

                //ImageInstruction result = await _service.UploadFile(filePathLoaded);
                ProductImages.Add(new CarouselCardItem(filePathLoaded, "test !"));
                ProductDetail.ProductImageInstruction.Add(new ImageInstruction(filePathLoaded, filePathLoaded, filePathLoaded));

                unitOfWork.ProductRepository.Update(ProductDetail);
               int changes =  unitOfWork.Completed();
            }
            catch (Exception ex)
            {
                throw new UploadFileException("Error uploading file for AtelierCremazie", ex);
            }
        }

        public void ReadOnlyCmd()
        {
            IsReadonly = !IsReadonly;
        }
    }
}