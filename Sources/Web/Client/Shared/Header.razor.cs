using Common.Helpers.RazorComponent;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Shared
{
    public partial class Header : PageLayoutComponentBase
    {
        //[Inject] private IProductWork productWorker { get; set; } = default!;

        public Product ProductDetail { get; set; }

        public void AddImageEventHandler()
        {
            //productWorker.ProductRepository.Add(new Product(ProductName));
        }

        public void test()
        {
            //OpenModal("NewProduct");
        }

    }
}
