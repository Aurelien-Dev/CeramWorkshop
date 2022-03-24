using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class FetchData
    {
        private IEnumerable<Product>? Products;
        private string? Name;

        [Inject] private IUnitOfWork _unitOfWork { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Products = await _unitOfWork.ProductRepository.GetAll();
        }

        public async Task AddProduct()
        {
            if (Name != null)
            {
                await _unitOfWork.ProductRepository.Add(new Product(Name));
                _unitOfWork.Completed();
            }

            Products = await _unitOfWork.ProductRepository.GetAll();
        }
        public void GoToProductDetail(int id)
        {
            _navigationManager.NavigateTo($"ProductDetail/{id}");
        }
    }
}