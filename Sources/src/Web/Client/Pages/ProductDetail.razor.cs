using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class ProductDetail
    {
        [Parameter] public int Id { get; set; }

        public Product? CurrentProduct { get; set; }
        [Inject] private IUnitOfWork _unitOfWork { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentProduct = await _unitOfWork.ProductRepository.Get(Id);
        }


    }
}
