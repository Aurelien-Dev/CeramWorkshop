using Client.Pages.ProductDetailPage.Dialogs;
using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Client.Utils.ComponentBase;
using Utils.Exception;

namespace Client.Pages.ProductDetailPage
{
    [Authorize]
    public partial class ProductDetailPage : CustomComponentBase
    {
        [Parameter] public int? Id { get; set; } = default!;
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;

        private Product ProductDetail { get; set; } = new();
        private ICollection<Material> Materials { get; set; } = default!;
        private ICollection<Firing> Firings { get; set; } = default!;


        private bool ShowCarouselNavigation { get; set; } = false;
        public bool ShowEditingImageButtons { get; set; } = true;
        private int CarouselSelectedIndex { get; set; } = 0;


        private double TotalMakingCost => ProductDetailPageComposition.GetTotalComposition() + ProductDetailPageFiring.GetTotalFiring();

        private ProductDetailPageComposition ProductDetailPageComposition { get; set; }
        private ProductDetailPageFiring ProductDetailPageFiring { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!Id.HasValue)
                throw new ParameterPageNullException(nameof(Id));

            await LoadData(Id.Value);

            RefreshCarouselInfo();
        }

        private void RefreshCarouselInfo()
        {
            ShowCarouselNavigation = ProductDetail.ImageInstructions.Count > 1;
            ShowEditingImageButtons = !ProductDetail.ImageInstructions.Any();
        }

        private async Task LoadData(int id)
        {
            ProductDetail = await ProductWorker.ProductRepository.Get(id, CurrentSession.Workshop.Id, ComponentDisposed);
            Materials = await ProductWorker.MaterialRepository.GetAll(ComponentDisposed);
            Firings = await ProductWorker.FiringRepository.GetAll(ComponentDisposed);
        }

        #region Image traitement

        private async Task OpenEditImageProductDialog()
        {
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail, ["ImageInstruction"] = this.ProductDetail.ImageInstructions.ElementAt(CarouselSelectedIndex) };

            var dialog = await DialogService.ShowAsync<ProductImageEditDialog>("Modifier le commentaire de l'image", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Canceled)
            {
                if (Id.HasValue)
                    await LoadData(Id.Value);
                return;
            }

            RefreshCarouselInfo();
        }

        private async Task OpenAddImageProductDialog()
        {
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail };

            var dialog = await DialogService.ShowAsync<ProductImageAddDialog>("Ajouter une photo", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Canceled) return;

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
            ProductWorker.ProductRepository.Update(ProductDetail);
            await ProductWorker.Completed(ComponentDisposed);

            if (ProductDetail.ImageInstructions.Any())
                CarouselSelectedIndex = ProductDetail.ImageInstructions.Count - 1;

            StateHasChanged();
            RefreshCarouselInfo();
        }

        private void OnToggledFavoriteChanged(bool toggled, ImageInstruction image)
        {
            ProductWorker.ImageInstructionRepository.SetNewFavorite(toggled, image.Id, ProductDetail.Id, ComponentDisposed);
            image.IsFavoriteImage = toggled;
        }

        #endregion

        #region Product traitement

        private async Task OpenDeleteProductDialog()
        {
            bool? result = await DialogService.ShowMessageBox("Suppression du produit", "Voulez-vous supprimer le produit ? suppression définitive.", yesText: "Delete!", cancelText: "Cancel");

            if (!result.HasValue) return;

            ProductWorker.ProductRepository.Delete(ProductDetail);
            await ProductWorker.Completed(ComponentDisposed);
            NavigationManager.NavigateTo($"/");
        }

        private async Task OpenEditProductDialog()
        {
            var parameters = new DialogParameters { ["ProductDetail"] = this.ProductDetail };

            var dialog = await DialogService.ShowAsync<ProductEditDetailDialog>("Modifier les détails du produit", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            if (result.Canceled && Id.HasValue) await LoadData(Id.Value);

            StateHasChanged();
        }

        #endregion
    }
}