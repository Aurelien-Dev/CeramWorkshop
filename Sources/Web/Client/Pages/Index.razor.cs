using Client.Utils;
using Common.Helpers.RazorComponent.CommonControls;
using Domain.Models;
using ExternalServices.ServicesUploadImage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Utils.Exception;

namespace Client.Pages
{
    public partial class Index : ComponentBase
    {
        public Product ProductDetail { get; set; }
        public bool IsReadonly { get; set; } = false;


        List<CardCarouselItem> ProductImages = new List<CardCarouselItem>();


        private List<IBrowserFile> loadedFiles = new();
        private ImgBBService _service;
        
        public Index()
        {            
            _service = new ImgBBService();
            ProductDetail = new Product("Vase");
            ProductDetail.Id = 1;
            ProductDetail.Reference = "ACV1";
            ProductDetail.Description = "Vase ming fait en grès de saint amand.";
            ProductDetail.DesignInstruction = "Insctruction de construction.";
            ProductDetail.BottomDiameter = 11;
            ProductDetail.TopDiameter = 2;
            ProductDetail.Height = 35;
            ProductDetail.Status = 1;
        }

        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            try
            {
                string filePathLoaded = await LoadFileFromInputFile.LoadFileInput(e, "AtelierCremazie");

                ImageInstruction result = await _service.UploadFile(filePathLoaded);
                ProductImages.Add(new CardCarouselItem(filePathLoaded, "test !"));
            }
            catch (Exception ex)
            {
                throw new UploadFileException("Error uploadeding file for AtelierCremazie", ex);
            }
        }

        public void ReadonlyCmd()
        {
            IsReadonly = !IsReadonly;
        }
    }
}