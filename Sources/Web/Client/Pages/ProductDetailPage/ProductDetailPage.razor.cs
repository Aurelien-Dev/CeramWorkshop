using Client.Pages.ProductDetailPage.Dialogs;
using Client.Utils;
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
        public bool ShowEditingImageButtons { get; set; } = true;

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
            ShowEditingImageButtons = !ProductDetail.ImageInstructions.Any();
        }

        private async Task LoadData(int id)
        {
            ProductDetail = await productWorker.ProductRepository.Get(id);
        }



        #region Image traitement
        private async Task OpenEditImageProductDialog()
        {
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail, ["ImageInstruction"] = this.ProductDetail.ImageInstructions.ElementAt(CarouselSelectedIndex) };

            var dialog = DialogService.Show<ProductImageEditDialog>("Modifier le commentaire de l'image", parameters, this.commonOptionDialog);
            var result = await dialog.Result;

            if (result.Cancelled) return;

            RefreshCarouselInfo();
        }

        private async Task OpenAddImageProductDialog()
        {
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail };

            var dialog = DialogService.Show<ProductImageAddDialog>("Ajouter une photo", parameters, this.commonOptionDialog);
            var result = await dialog.Result;

            if (result.Cancelled) return;

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

            if (ProductDetail.ImageInstructions.Any())
                CarouselSelectedIndex = ProductDetail.ImageInstructions.Count - 1;

            StateHasChanged();
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
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail };

            var dialog = DialogService.Show<ProductEditDetailDialog>("Modifier les détails du produit", parameters, this.commonOptionDialog);

            var result = await dialog.Result;
            if (result.Cancelled) return;

            StateHasChanged();
        }
        #endregion
    }
}