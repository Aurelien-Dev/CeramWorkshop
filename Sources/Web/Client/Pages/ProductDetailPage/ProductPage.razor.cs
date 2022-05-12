using Client.Utils;
using Common.Helpers.RazorComponent;
using Domain.Interfaces;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;
using Utils.Exception;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductPage : PageComponentBase
    {
        [Parameter] public int? Id { get; set; } = default!;
        [Inject] private IProductWork productWorker { get; set; } = default!;
        [Inject] private IMaterialRepository MaterialRepository { get; set; } = default!;

        [NotNull] public Product ProductDetail = new();
        [NotNull] public Material MaterialDetail = new();

        private int _selectedImageId;
        private bool _editedPhoto = false;
        public ImageInstruction ImageInstruction { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (!Id.HasValue)
                throw new ArgumentNullException(nameof(Id));

            Mode = PageMode.Edit;
            await LoadData(Id.Value);
        }

        private void ModalConfirmDelete()
        {
            productWorker.ProductRepository.Delete(ProductDetail);
            int nbr = productWorker.Completed();
            NavigationManager.NavigateTo($"/");
        }

        private async Task LoadData(int id)
        {
            ProductDetail = await productWorker.ProductRepository.Get(id);
        }



        #region Edit product details
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
        public async Task UndoEditProductDetail()
        {
            if (!Id.HasValue)
                throw new ArgumentNullException(nameof(Id));

            productWorker.Rollback();
            await LoadData(Id.Value);
        }
        #endregion


        #region Image
        public void AddImageEventHandler(EditContext context)
        {
            ProductDetail.ImageInstructions.Add(ImageInstruction);
            productWorker.Completed();
            ResetImageClick();
        }

        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            try
            {
                string filePathLoaded = await LoadFileFromInputFile.LoadFileInput(e, "AtelierCremazie");
                filePathLoaded = filePathLoaded.Replace(@"\", "/");

                ImageInstruction.Url = filePathLoaded;
                ImageInstruction.ThumbUrl = filePathLoaded;
                ImageInstruction.MediumUrl = filePathLoaded;
            }
            catch (Exception ex)
            {
                throw new UploadFileException("Error uploading file for AtelierCremazie", ex);
            }
        }

        public void DeleteImageCmd()
        {
            ImageInstruction deleteImage = ProductDetail.ImageInstructions.FirstOrDefault(i => i.Id == _selectedImageId);
            if (deleteImage != null)
            {
                ProductDetail.ImageInstructions.Remove(deleteImage);
            }
            productWorker.Completed();
        }

        public void EditImageCmd()
        {
            ProductDetail.ImageInstructions.Where(i => i.Id == ImageInstruction.Id).First().Comment = ImageInstruction.Comment;
            productWorker.Completed();
            ResetImageClick();
        }

        #region Modales
        public void OpenDeleteImageModal(int idImage)
        {
            _selectedImageId = idImage;
            OpenModal("ConfirmDeleteImageModal");
        }

        public void OpenEditImageModal(int idImage)
        {
            _editedPhoto = true;
            ImageInstruction editImage = ProductDetail.ImageInstructions.Where(i => i.Id == idImage).FirstOrDefault();
            if (editImage != null)
            {
                ImageInstruction.Id = editImage.Id;
                ImageInstruction.Url = editImage.Url;
                ImageInstruction.MediumUrl = editImage.MediumUrl;
                ImageInstruction.ThumbUrl = editImage.ThumbUrl;
                ImageInstruction.Comment = editImage.Comment;
                OpenModal("AddImageModal");
            }
        }
        #endregion

        public void ResetImageClick()
        {
            ImageInstruction = new();
        }
        #endregion

        #region Material
        public async Task AddMaterialEventHandler(EditContext context)
        {
            MaterialRepository.AddAndLinkMaterial(MaterialDetail, ProductDetail);
        }
        #endregion
    }
}