using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage
{
    public partial class ListPage : ComponentBase
    {
        [Inject] private IProductWork unitOfWork { get; set; }

        public IList<ProductViewModel> ProductsVM { get; set; } = new List<ProductViewModel>();


        protected override async Task OnInitializedAsync()
        {
            IEnumerable<Product> Products = await unitOfWork.ProductRepository.GetAll();

            foreach (var product in Products)
            {
                int imgCount = unitOfWork.ProductRepository.CountImage(product.Id);

                ProductsVM.Add(new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Reference = product.Reference,
                    CountImg = imgCount
                });
            }
        }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Name { get; set; }
        public int CountImg { get; set; }
    }
}