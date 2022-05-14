using Client.Pages.ProductDetailPage.Modals;
using Client.Utils;
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

        [NotNull] public Product ProductDetail { get; set; } = new();
        [NotNull] public Material MaterialDetail { get; set; } = new();

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


        public async Task OpenModal()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("IdProduct", Id.Value);

            ModalInstance instance = ModalService.OpenModal<ProductDetailModal>("Edit", data);

            instance.ModalClosed = (confirm) =>
            {
                if (confirm)
                {
                    StateHasChanged();
                }
            };
        }



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