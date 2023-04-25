using Client.Pages.ProductDetailPage.Components.ViewModel;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage
{
    public enum OrderingPage { StatusAsc, StatusDesc, NameAsc, NameDesc }

    [Authorize]
    public partial class ProductListPage : CustomComponentBase, IDisposable
    {
        [Inject] private IProductWorker UnitOfWork { get; set; } = default!;

        private IEnumerable<Product> Products { get; set; } = new List<Product>();
        private IList<ProductListItemViewModel> ProductsVm { get; set; } = new List<ProductListItemViewModel>();
        private string Search { get; set; } = string.Empty;
        private OrderingPage SelectedOrder { get; set; } = OrderingPage.NameAsc;

        protected override async Task OnInitializedAsync()
        {
            Products = await UnitOfWork.ProductRepository.GetAll(CurrentSession.Workshop.Id);
            ProductsVm = new List<ProductListItemViewModel>(Products.Select(p => new ProductListItemViewModel(p)).OrderBy(p => p.Name).ToList());
        }

        private void RaiseOrdering(OrderingPage e)
        {
            SelectedOrder = e;
            FilterProduct();
        }

        private void RaiseFilter(string e)
        {
            Search = e;
            FilterProduct();
        }

        private void FilterProduct()
        {
            IQueryable<Product> ex = Products.AsQueryable();
            
            ex = SearchProduct(ex, Search);

            ex = SelectedOrder switch
            {
                OrderingPage.StatusAsc => ex.OrderBy(p => p.Status),
                OrderingPage.StatusDesc => ex.OrderByDescending(p => p.Status),
                OrderingPage.NameAsc => ex.OrderBy(p => p.Name),
                OrderingPage.NameDesc => ex.OrderByDescending(p => p.Name),
                _ => throw new ArgumentOutOfRangeException(nameof(SelectedOrder), SelectedOrder, null)
            };

            ProductsVm = ex.Select(p => new ProductListItemViewModel(p)).ToList();
        }
        
        private static IQueryable<Product> SearchProduct(IQueryable<Product> query, string value)
        {
            if (string.IsNullOrEmpty(value)) return query;

            return query.Where(p => p.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
                                    p.Reference.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Dispose()
        {
            UnitOfWork.Close();
        }
    }
}