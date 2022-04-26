using Client.ViewModel.ProductDetailViewModel;
using Common.Helpers.RazorComponent;
using Common.Helpers.RazorComponent.CommonControls;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;


namespace Client.Pages.ProductDetailPage
{

    public partial class ProductPage : PageComponentBase
    {
        [Parameter] public int? Id { get; set; }
        [Inject] private IProductWork productWorker { get; set; }

        public ProductViewModel ProductDetailVM { get; set; } = new();
        public List<ImageInstruction> ProductImages = new();

        private Product _product;

        protected override async Task OnInitializedAsync()
        {
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