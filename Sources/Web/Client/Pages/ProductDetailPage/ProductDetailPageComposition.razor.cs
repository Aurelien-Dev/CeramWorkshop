using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductDetailPageComposition : CustomComponentBase
    {
        [Inject] private IProductWorker Worker { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ICollection<Material> Materials { get; set; } = default!;

        ICollection<MaterialViewModel> MaterialsVM { get; set; } = new List<MaterialViewModel>();
        MudAutocomplete<Material> AutocompleteBox = new();
        double TotalComposition { get => MaterialsVM.Sum(m => m.TotalCost); }

        protected override void OnParametersSet()
        {
            MaterialsVM = new List<MaterialViewModel>();
            foreach (var pMat in ProductDetail.ProductMaterial)
            {
                MaterialsVM.Add(new MaterialViewModel(pMat));
            }
        }

        private async Task SelectedValueChanged(Material mat)
        {
            if (mat == null) return;

            await AutocompleteBox.Clear();

            var pMat = new ProductMaterial(mat.Id, ProductDetail.Id, 0, mat.Cost) { Material = mat };

            MaterialsVM.Add(new MaterialViewModel(pMat));
            ProductDetail.ProductMaterial.Add(pMat);
            Worker.Completed();

            StateHasChanged();
        }

        public void CostChanged(double cost, int id)
        {
            ProductMaterial pmToUpdate = ProductDetail.ProductMaterial.FirstOrDefault(p => p.Id == id);

            if (pmToUpdate == null) return;
            if (cost == pmToUpdate.Cost) return;

            pmToUpdate.Cost = cost;
            Worker.ProductRepository.UpdateProductMaterial(pmToUpdate);
            CalculateTotalCost(id);
        }

        public void QuantityChanged(double quantity, int id)
        {
            ProductMaterial pmToUpdate = ProductDetail.ProductMaterial.FirstOrDefault(p => p.Id == id);

            if (pmToUpdate == null) return;
            if (quantity == pmToUpdate.Quantity) return;

            pmToUpdate.Quantity = quantity;
            Worker.ProductRepository.UpdateProductMaterial(pmToUpdate);
            CalculateTotalCost(id);
        }

        private async Task<IEnumerable<Material>> Search(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return Materials;

            return Materials
                .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
                x.Reference.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void CalculateTotalCost(int id)
        {
            MaterialViewModel pmVMToUpdate = MaterialsVM.FirstOrDefault(p => p.PMat.Id == id);
            pmVMToUpdate.CalculateCost();
        }

        private async Task DeleteMat(MaterialViewModel materialVM)
        {
            ProductDetail.ProductMaterial.Remove(materialVM.PMat);
            Worker.ProductRepository.Update(ProductDetail);
            Worker.Completed();

            MaterialsVM.Remove(materialVM);
            StateHasChanged();
        }
    }

    public class MaterialViewModel
    {
        public ProductMaterial PMat { get; set; }
        public double TotalCost { get; set; }
        public string Unite
        {
            get
            {
                switch (PMat.Material.UniteMesure)
                {
                    case MaterialUnite.Kg:
                        return "gr";
                    case MaterialUnite.L:
                        return "ml";
                    case MaterialUnite.Unit:
                        return "/p";
                    default:
                        return "";
                }
            }
        }

        public MaterialViewModel(ProductMaterial pMat)
        {
            PMat = pMat;
            CalculateCost();

        }

        public void CalculateCost()
        {
            TotalCost = PMat.CalculatedCost;
        }
    }
}