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

        public Product ProductDetail { get; set; } = new();
        public List<CarouselCardItem> ProductImages = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadData(Id.Value);
        }

        private async Task LoadData(int id)
        {
            ProductDetail = await productWorker.ProductRepository.Get(id);
            ProductImages = new();

            foreach (var item in ProductDetail.ProductImageInstruction)
            {
                ProductImages.Add(new CarouselCardItem(item.Id, item.Url, item.Comment, item));
            }
        }
    }
}