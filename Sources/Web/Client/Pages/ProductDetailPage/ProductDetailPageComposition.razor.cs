using Client.Pages.ProductDetailPage.Dialogs;
using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductDetailPageComposition : PageComponentBase
    {
        [Inject] private IProductWork Worker { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = new();
        [Parameter] public ICollection<Material> Materials { get; set; } = default!;

        MudAutocomplete<Material> AutocompleteBox = new();

        private async Task SelectedValueChanged(Material mat)
        {
            if (mat == null) return;

            var parameters = new DialogParameters { ["MaterialDetail"] = mat };

            var dialog = DialogService.Show<ProductMaterialLinkDialog>("Add link", parameters, this.CommonOptionDialog);
            var result = await dialog.Result;

            await AutocompleteBox.Clear();

            if (result.Cancelled) return;

            ProductDetail.ProductMaterial.Add((ProductMaterial)result.Data);
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
        }

        public void QuantityChanged(double quantity, int id)
        {
            ProductMaterial pmToUpdate = ProductDetail.ProductMaterial.FirstOrDefault(p => p.Id == id);

            if (pmToUpdate == null) return;
            if (quantity == pmToUpdate.Quantity) return;

            pmToUpdate.Quantity = quantity;
            Worker.ProductRepository.UpdateProductMaterial(pmToUpdate);
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
    }
}