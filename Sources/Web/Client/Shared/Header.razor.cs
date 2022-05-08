using Common.Helpers.RazorComponent;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Shared
{
    public partial class Header : PageLayoutComponentBase
    {
        [Inject] private IProductWork productWorker { get; set; } = default!;

        public Product ProductDetail { get; set; } = new();

        public void AddImageEventHandler()
        {
            ProductDetail.Reference = "New";
            productWorker.ProductRepository.Add(ProductDetail);
            productWorker.Completed();

            NavigationManager.NavigateTo($"/Product/{ProductDetail.Id}", true);
            ProductDetail = new();
        }
    }
}