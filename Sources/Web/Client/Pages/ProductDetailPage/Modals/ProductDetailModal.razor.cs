using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages.ProductDetailPage.Modals
{
    public partial class ProductDetailModal : ModalComponentBase
    {
        [Inject] private IProductWork productWorker { get; set; } = default!;
        [Parameter] public int IdProduct { get; set; }
        public ProductModalViewModel ProductDetailVM { get; set; } = new();


        private Product _product { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            _product = await productWorker.ProductRepository.GetLight(IdProduct);
            ProductDetailVM = new(_product);
        }

        #region Edit product details
        public void SaveProductEventHandler(EditContext context)
        {
            bool isValid = context.Validate();
            if (isValid)
            {
                _product.Reference = ProductDetailVM.Reference;
                _product.Name = ProductDetailVM.Name;
                _product.Description = ProductDetailVM.Description;
                _product.Height = ProductDetailVM.Height;
                _product.TopDiameter = ProductDetailVM.TopDiameter;
                _product.BottomDiameter = ProductDetailVM.BottomDiameter;
                _product.Status = ProductDetailVM.Status;

                productWorker.ProductRepository.Update(_product);
                int result = productWorker.Completed();
            }
            Close(true);
        }

        public async Task UndoEditProductDetail()
        {
            Close(false);
        }
        #endregion
    }

    public class ProductModalViewModel
    {
        public ProductModalViewModel() { }

        public ProductModalViewModel(Product product)
        {
            Reference = product.Reference;
            Name = product.Name;
            Description = product.Description;
            DesignInstruction = product.DesignInstruction;
            Height = product.Height;
            TopDiameter = product.TopDiameter;
            BottomDiameter = product.BottomDiameter;
            Status = product.Status;
        }

        public string Reference { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DesignInstruction { get; set; }
        public double? Height { get; set; }
        public double? TopDiameter { get; set; }
        public double? BottomDiameter { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
