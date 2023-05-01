using Client.Utils;
using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage
{
    [Authorize]
    public partial class ProductDetailPageFiring : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ICollection<Firing> Firings { get; set; } = default!;

        private ICollection<FiringViewModel> FiringsVm { get; set; } = new List<FiringViewModel>();
        private MudAutocomplete<Firing> _autocompleteBox = new();
        private double TotalFiring => FiringsVm.Sum(m => m.UnitaryCost);


        protected override void OnParametersSet()
        {
            FiringsVm = new List<FiringViewModel>();
            foreach (var pFire in ProductDetail.ProductFiring)
            {
                FiringsVm.Add(new FiringViewModel(pFire));
            }
        }

        public double GetTotalFiring()
        {
            return TotalFiring;
        }

        private async Task SelectedValueChanged(Firing? fire)
        {
            if (fire == null) return;

            await _autocompleteBox.Clear();

            var pFire = new ProductFiring(fire.Id, ProductDetail.Id, fire.TotalKwH, fire.CostKwH) { Firing = fire };

            FiringsVm.Add(new FiringViewModel(pFire));
            ProductDetail.ProductFiring.Add(pFire);
            await ProductWorker.Completed(ComponentDisposed);

            StateHasChanged();
        }

        public void CostChanged(int number, int id)
        {
            ProductFiring pfToUpdate = ProductDetail.ProductFiring.FirstOrDefault(p => p.Id == id);

            if (pfToUpdate == null) return;
            if (number == pfToUpdate.NumberProducts) return;

            pfToUpdate.NumberProducts = number;
            ProductWorker.ProductRepository.UpdateProductFiring(pfToUpdate, ComponentDisposed);

            // CalculateTotalCost(pfToUpdate.Id, pfToUpdate);
        }

        private async Task<IEnumerable<Firing>> Search(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return Firings;

            return Firings
                .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) );
        }

        private void DeleteFire(FiringViewModel firingVm)
        {
            ProductDetail.ProductFiring.Remove(firingVm.ProductFire);
            ProductWorker.ProductRepository.Update(ProductDetail);
            ProductWorker.Completed(ComponentDisposed);

            FiringsVm.Remove(firingVm);
            StateHasChanged();
        }

        // private void CalculateTotalCost(int id, ProductFiring pfToUpdate)
        // {
        //     FiringViewModel pmVMToUpdate = FiringsVm.FirstOrDefault(p => p.ProductFire.Id == id);
        // }
    }

    public class FiringViewModel
    {
        public ProductFiring ProductFire { get; set; }
        public double UnitaryCost => ProductFire.CostKwH * ProductFire.Firing.TotalKwH / ProductFire.NumberProducts;

        public FiringViewModel(ProductFiring productFire)
        {
            ProductFire = productFire;
        }
    }
}