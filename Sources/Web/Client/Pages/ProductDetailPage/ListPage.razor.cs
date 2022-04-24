using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage
{
    public partial class ListPage : ComponentBase
    {
        [Inject] private IProductWork unitOfWork { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();


        protected override async Task OnInitializedAsync()
        {
            Products = await unitOfWork.ProductRepository.GetAll();
        }
    }
}