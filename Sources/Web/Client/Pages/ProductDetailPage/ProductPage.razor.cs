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
        public List<CarouselCardItem> ProductImages = new();

        private void OnDeleteDialogClose(bool accepted)
        {
            
        }

        private void OpenDeleteDialog()
        {
            OpenModal("exampleModal");
        }


        protected override async Task OnInitializedAsync()
        {
            await LoadData(Id.Value);
        }

        private async Task LoadData(int id)
        {
            Product product = await productWorker.ProductRepository.Get(id);
            ProductDetailVM = new(product);

            ProductImages = new();

            foreach (var item in product.ProductImageInstruction)
            {
                ProductImages.Add(new CarouselCardItem(item.Id, item.Url, item.Comment, item));
            }
        }

        public void GoToEdit() => NavigationManager.NavigateTo($"/Product/Edit/{Id}");
    }
}