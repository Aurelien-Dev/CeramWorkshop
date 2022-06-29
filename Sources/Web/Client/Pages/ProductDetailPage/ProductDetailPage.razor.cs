using Client.Pages.ProductDetailPage.Dialogs;
using Client.Utils;
using Domain.Interfaces;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics.CodeAnalysis;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductDetailPage : PageComponentBase
    {
        [Parameter] public int? Id { get; set; } = default!;
        [Inject] private IProductWork productWorker { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        [NotNull] public Product ProductDetail { get; set; } = new();
        [NotNull] public Material MaterialDetail { get; set; } = new();


        public ImageInstruction ImageInstruction { get; set; } = new();

        public bool ShowCarouselNavigation { get; set; } = false;
        public bool ShowDeleteImageButton { get; set; } = true;

        public int CarouselSelectedIndex { get; set; } = 0;


        protected override async Task OnInitializedAsync()
        {
            if (!Id.HasValue)
                throw new ArgumentNullException(nameof(Id));

            await LoadData(Id.Value);

            RefreshCarouselInfo();
        }

        public void RefreshCarouselInfo()
        {
            ShowCarouselNavigation = ProductDetail.ImageInstructions.Count > 1;
            ShowDeleteImageButton = !ProductDetail.ImageInstructions.Any();
            StateHasChanged();
        }

        private async Task LoadData(int id)
        {
            ProductDetail = await productWorker.ProductRepository.Get(id);
        }



        #region Image traitement
        private async Task OpenEditImageProductDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail, ["ImageInstruction"] = this.ProductDetail.ImageInstructions.ElementAt(CarouselSelectedIndex) };

            var dialog = DialogService.Show<ProductImageEdit>("Modifier le commentaire de l'image", parameters, options);

            RefreshCarouselInfo();
        }

        private async Task OpenAddImageProductDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail };

            var dialog = DialogService.Show<ProductImageAdd>("Ajouter une photo", parameters, options);

            CarouselSelectedIndex = ProductDetail.ImageInstructions.Count - 1;
            RefreshCarouselInfo();
        }

        private async Task OpenDeleteImageProductDialog()
        {
            bool? result = await DialogService.ShowMessageBox("Suppression cette photo", "Voulez-vous supprimer la photo ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            var image = ProductDetail.ImageInstructions.ElementAt(CarouselSelectedIndex);
            LoadFileFromInputFile.RemoveFileInput(image.Url);

            ProductDetail.ImageInstructions.Remove(image);
            productWorker.ProductRepository.Update(ProductDetail);
            productWorker.Completed();

            RefreshCarouselInfo();
        }
        #endregion

        #region Product traitement
        private async Task OpenDeleteProductDialog()
        {
            bool? result = await DialogService.ShowMessageBox("Suppression du produit", "Voulez-vous supprimer le produit ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            productWorker.ProductRepository.Delete(ProductDetail);
            productWorker.Completed();
            NavigationManager.NavigateTo($"/");
        }

        private async Task OpenEditProductDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail };

            var dialog = DialogService.Show<ProductEditDetail>("Modifier les détails du produit", parameters, options);
        }
        #endregion
    }
}