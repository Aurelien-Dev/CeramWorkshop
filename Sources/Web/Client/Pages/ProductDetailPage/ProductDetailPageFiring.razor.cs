using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductDetailPageFiring : CustomComponentBase
    {
        [Inject] private IProductWorker Worker { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ICollection<Firing> Firings { get; set; } = default!;

        ICollection<FiringViewModel> FiringsVM { get; set; } = new List<FiringViewModel>();
        MudAutocomplete<Firing> AutocompleteBox = new();
        double TotalComposition { get => FiringsVM.Sum(m => m.Pfire.CostKwH); }


        protected override void OnParametersSet()
        {
            FiringsVM = new List<FiringViewModel>();
            foreach (var pFire in ProductDetail.ProductFiring)
            {
                FiringsVM.Add(new FiringViewModel(pFire));
                CalculateTotalCost(pFire.Id, pFire);
            }
        }

        private async Task SelectedValueChanged(Firing fire)
        {
            if (fire == null) return;

            await AutocompleteBox.Clear();

            var pFire = new ProductFiring(fire.Id, ProductDetail.Id, fire.TotalKwH, 0) { Firing = fire };

            FiringsVM.Add(new FiringViewModel(pFire));
            ProductDetail.ProductFiring.Add(pFire);
            Worker.Completed();

            StateHasChanged();
        }

        public void CostChanged(double cost, int id)
        {
            ProductFiring pfToUpdate = ProductDetail.ProductFiring.FirstOrDefault(p => p.Id == id);

            if (pfToUpdate == null) return;
            if (cost == pfToUpdate.CostKwH) return;

            pfToUpdate.CostKwH = cost;
            Worker.ProductRepository.UpdateProductFiring(pfToUpdate);

            CalculateTotalCost(pfToUpdate.Id, pfToUpdate);
        }

        private async Task<IEnumerable<Firing>> Search(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return Firings;

            return Firings
                .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) );
        }

        private void DeleteFire(FiringViewModel firingVM)
        {
            ProductDetail.ProductFiring.Remove(firingVM.Pfire);
            Worker.ProductRepository.Update(ProductDetail);
            Worker.Completed();

            FiringsVM.Remove(firingVM);
            StateHasChanged();
        }
        private void CalculateTotalCost(int id, ProductFiring pfToUpdate)
        {
            FiringViewModel pmVMToUpdate = FiringsVM.FirstOrDefault(p => p.Pfire.Id == id);
            pmVMToUpdate.TotalCost = pfToUpdate.Firing.TotalKwH * pfToUpdate.CostKwH;
        }
    }

    public class FiringViewModel
    {
        public ProductFiring Pfire { get; set; }
        public double TotalCost { get; set; }

        public FiringViewModel(ProductFiring pFire)
        {
            Pfire = pFire;
        }
    }
}