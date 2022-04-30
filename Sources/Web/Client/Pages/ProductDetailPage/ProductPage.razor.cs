using Client.ViewModel.ProductDetailViewModel;
using Common.Helpers.RazorComponent;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Client.Pages.ProductDetailPage
{

    public partial class ProductPage : PageComponentBase
    {
        [Parameter] public int? Id { get; set; } = default!;
        [Inject] private IProductWork productWorker { get; set; } = default!;

        public ProductViewModel ProductDetailVM { get; set; } = new();
        public List<ImageInstruction> ProductImages = new();

        [NotNull] private Product _product = new();

        protected override async Task OnInitializedAsync()
        {
            if (!Id.HasValue)
                throw new ArgumentNullException(nameof(Id));

            await LoadData(Id.Value);
        }

        private void ModalConfirmDelete()
        {
            productWorker.ProductRepository.Delete(_product);
            int nbr = productWorker.Completed();
            NavigationManager.NavigateTo($"/");
        }

        private async Task LoadData(int id)
        {
            _product = await productWorker.ProductRepository.Get(id);
            ProductDetailVM = new(_product);
            ProductImages = new(_product.ImageInstructions);
        }

        public void GoToEdit() => NavigationManager.NavigateTo($"/Product/Edit/{Id}");
    }
}