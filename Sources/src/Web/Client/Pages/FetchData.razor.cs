using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class FetchData
    {
        private IEnumerable<Product>? Products;
        private string? Name;

        [Inject] private IProductWork unitOfWork { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Products = await unitOfWork.ProductRepository.GetAll();
        }

        public async Task AddProduct()
        {
            if (Name != null)
            {
                await unitOfWork.ProductRepository.Add(new Product(Name));
                unitOfWork.Completed();
            }

            Products = await unitOfWork.ProductRepository.GetAll();
        }
        public void GoToProductDetail(int id)
        {
            navigationManager.NavigateTo($"ProductDetail/{id}");
        }
    }
}