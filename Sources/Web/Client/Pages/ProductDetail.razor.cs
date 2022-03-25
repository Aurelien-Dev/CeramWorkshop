using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class ProductDetail
    {
        [Parameter] public int Id { get; set; }
        [Inject] private IProductWork unitOfWork { get; set; }

        public Product? CurrentProduct { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentProduct = await unitOfWork.ProductRepository.Get(Id);
        }
    }
}