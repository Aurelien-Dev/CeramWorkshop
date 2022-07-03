using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Client.Pages.ProductDetailPage
{
    public enum OrderingPage { StatusAsc, StatusDesc, NameAsc, NameDesc }
    public partial class ProductListPage : ComponentBase
    {
        [Inject] private IProductWork unitOfWork { get; set; } = default!;

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IList<ProductViewModel> ProductsVM { get; set; } = new List<ProductViewModel>();
        public string Search { get; set; } = string.Empty;
        public OrderingPage SelectedOrder { get; set; } = OrderingPage.NameAsc;

        protected override async Task OnInitializedAsync()
        {
            Products = await unitOfWork.ProductRepository.GetAll();
            ProductsVM = new List<ProductViewModel>(Products.Select(p => new ProductViewModel(p)).OrderBy(p => p.Name).ToList());
        }

        private void RaiseOrdering(OrderingPage e)
        {
            SelectedOrder = e;
            IQueryable<Product> ex = Products.AsQueryable();

            ex = Filterproducts(ex, Search);

            switch (e)
            {
                case OrderingPage.StatusAsc:
                    ex = ex.OrderBy(p => p.Status);
                    break;
                case OrderingPage.StatusDesc:
                    ex = ex.OrderByDescending(p => p.Status);
                    break;
                case OrderingPage.NameAsc:
                    ex = ex.OrderBy(p => p.Name);
                    break;
                case OrderingPage.NameDesc:
                    ex = ex.OrderByDescending(p => p.Name);
                    break;
                default:
                    break;
            }

            ProductsVM = ex.Select(p => new ProductViewModel(p)).ToList();
        }

        private void RaisFilter(string e)
        {
            Search = e;
            IQueryable<Product> ex = Products.AsQueryable();

            ex = Filterproducts(ex, e);

            switch (SelectedOrder)
            {
                case OrderingPage.StatusAsc:
                    ex = ex.OrderBy(p => p.Status);
                    break;
                case OrderingPage.NameAsc:
                    ex = ex.OrderBy(p => p.Name);
                    break;
                default:
                    break;
            }

            ProductsVM = ex.Select(p => new ProductViewModel(p)).ToList();
        }

        private static IQueryable<Product> Filterproducts(IQueryable<Product> query, string value)
        {
            if (string.IsNullOrEmpty(value)) return query;

            return query.Where(p => p.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
                                    p.Reference.Contains(value, StringComparison.InvariantCultureIgnoreCase));
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