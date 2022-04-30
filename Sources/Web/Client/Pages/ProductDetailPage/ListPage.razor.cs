using Client.ViewModel.ProductDetailViewModel;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage
{
    public partial class ListPage : ComponentBase
    {
        [Inject] private IProductWork unitOfWork { get; set; } = default!;

        public IList<ProductViewModel> ProductsVM { get; set; } = new List<ProductViewModel>();


        protected override async Task OnInitializedAsync()
        {
            IEnumerable<Product> Products = await unitOfWork.ProductRepository.GetAll();

            foreach (var product in Products)
            {
                int imgCount = await unitOfWork.ProductRepository.CountImage(product.Id);

                ProductsVM.Add(new ProductViewModel(product) { CountImg = imgCount });
            }
        }
    }
}