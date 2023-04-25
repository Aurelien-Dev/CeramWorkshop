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
    public partial class ProductDetailPageComposition : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ICollection<Material> Materials { get; set; } = default!;

        private ICollection<MaterialViewModel> MaterialsVm { get; set; } = new List<MaterialViewModel>();
        private MudAutocomplete<Material>? _autocompleteBox = new();
        private double TotalComposition => MaterialsVm.Sum(m => m.TotalCost);

        protected override void OnParametersSet()
        {
            MaterialsVm = new List<MaterialViewModel>();
            foreach (var pMat in ProductDetail.ProductMaterial)
            {
                MaterialsVm.Add(new MaterialViewModel(pMat));
            }
        }

        public double GetTotalComposition()
        {
            return TotalComposition;
        }

        private async Task SelectedValueChanged(Material? mat)
        {
            if (mat == null) return;

            await _autocompleteBox.Clear();

            var pMat = new ProductMaterial(mat.Id, ProductDetail.Id, 0, mat.Cost) { Material = mat };

            MaterialsVm.Add(new MaterialViewModel(pMat));
            ProductDetail.ProductMaterial.Add(pMat);
            await ProductWorker.Completed();

            StateHasChanged();
        }

        public void CostChanged(double cost, int id)
        {
            ProductMaterial pmToUpdate = ProductDetail.ProductMaterial.FirstOrDefault(p => p.Id == id);

            if (pmToUpdate == null) return;
            if (cost == pmToUpdate.Cost) return;

            pmToUpdate.Cost = cost;
            ProductWorker.ProductRepository.UpdateProductMaterialCostAndQuantity(pmToUpdate);
            CalculateTotalCost(id);
        }

        public void QuantityChanged(double quantity, int id)
        {
            ProductMaterial pmToUpdate = ProductDetail.ProductMaterial.FirstOrDefault(p => p.Id == id);

            if (pmToUpdate == null) return;
            if (quantity == pmToUpdate.Quantity) return;

            pmToUpdate.Quantity = quantity;
            ProductWorker.ProductRepository.UpdateProductMaterialCostAndQuantity(pmToUpdate);
            CalculateTotalCost(id);
        }

        private async Task<IEnumerable<Material>> Search(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return Materials;

            return Materials.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
                                        x.Reference.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void CalculateTotalCost(int id)
        {
            MaterialViewModel pmVmToUpdate = MaterialsVm.FirstOrDefault(p => p.PMat.Id == id);

            if (pmVmToUpdate == null)
                throw new ArgumentException("Material not found.");
        }

        private void DeleteMat(MaterialViewModel materialVm)
        {
            ProductDetail.ProductMaterial.Remove(materialVm.PMat);
            ProductWorker.ProductRepository.Update(ProductDetail);
            ProductWorker.Completed();

            MaterialsVm.Remove(materialVm);
            StateHasChanged();
        }

        public  override void Dispose()
        {
            ProductWorker.Close();
        }
    }

    public class MaterialViewModel
    {
        public ProductMaterial PMat { get; }
        public double TotalCost => PMat.CalculatedCost;

        public string Unite
        {
            get
            {
                return PMat.Material.UniteMesure switch
                {
                    MaterialUnite.Kg => "gr",
                    MaterialUnite.L => "ml",
                    MaterialUnite.Unit => "/p",
                    _ => string.Empty
                };
            }
        }

        public MaterialViewModel(ProductMaterial pMat)
        {
            PMat = pMat;
        }
    }
}