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

        protected override void OnParametersSet()
        {
            foreach (var pMat in ProductDetail.ProductMaterial)
            {
                MaterialsVM.Add(new MaterialViewModel(pMat));
                CalculateTotalCost(pMat.Id, pMat);
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
            CalculateTotalCost(id, pmToUpdate);
        }

        public void QuantityChanged(double quantity, int id)
        {
            ProductMaterial pmToUpdate = ProductDetail.ProductMaterial.FirstOrDefault(p => p.Id == id);

            if (pmToUpdate == null) return;
            if (quantity == pmToUpdate.Quantity) return;

            pmToUpdate.Quantity = quantity;
            Worker.ProductRepository.UpdateProductMaterial(pmToUpdate);
            CalculateTotalCost(id, pmToUpdate);
        }

        private async Task<IEnumerable<Material>> Search1(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return Materials;

            return Materials
                .Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
                x.Reference.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private void CalculateTotalCost(int id, ProductMaterial pmToUpdate)
        {
            MaterialViewModel pmVMToUpdate = MaterialsVM.FirstOrDefault(p => p.PMat.Id == id);
            pmVMToUpdate.TotalCost = pmToUpdate.Cost / (pmToUpdate.Material.Quantity * 1000) * pmToUpdate.Quantity;
        }
    }

    public class MaterialViewModel
    {
        public ProductMaterial PMat { get; set; }
        public double TotalCost { get; set; }

        public MaterialViewModel(ProductMaterial pMat)
        {
            PMat = pMat;
        }
    }
}