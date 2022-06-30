using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductListPage : ComponentBase
    {
        [Inject] private IProductWork unitOfWork { get; set; } = default!;

        public IList<ProductViewModel> ProductsVM { get; set; } = new List<ProductViewModel>();

        protected override async Task OnInitializedAsync()
        {
            IEnumerable<Product> Products = await unitOfWork.ProductRepository.GetAll();
            ProductsVM = new List<ProductViewModel>(Products.Select(p => new ProductViewModel(p)).ToList());
        }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string Name { get; set; }
        public string Image { get; set; } 
        public ProductStatus Status { get; set; }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Reference = product.Reference;
            Name = product.Name;
            Status = product.Status;

            if (product.ImageInstructions.Any())
                Image = product.ImageInstructions.First().ThumbUrl;
            else
                Image = "assets/img/gallery.png";
        }
    }

}