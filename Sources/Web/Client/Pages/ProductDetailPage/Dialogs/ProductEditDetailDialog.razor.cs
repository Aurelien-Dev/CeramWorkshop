using Client.Utils.ComponentBase;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductEditDetailDialog : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Product ProductDetail { get; set; } = default!;
        private Product OriginalProductDetail { get; set; } = default!;
        [Parameter] public bool? InsertMode { get; set; } = new();

        private MudForm _form = new();
        private bool _success;
        private string[] _errors = Array.Empty<string>();
        private bool _loading = false;

        protected override void OnParametersSet()
        {
            OriginalProductDetail = ProductDetail.GetClone();
        }

        private async Task OnValidSubmit()
        {
            _loading = true;
            StateHasChanged();
            await _form.Validate();

            if (_form.IsValid)
            {
                await Task.Delay(350);
                ProductDetail.IdWorkshop = CurrentSession.Workshop.Id;
                if (InsertMode.HasValue && InsertMode.Value)
                    await ProductWorker.ProductRepository.Add(ProductDetail, ComponentDisposed);
                else
                {
                    await ProductWorker.ProductRepository.Update(ProductDetail, ComponentDisposed);
                }

                StateHasChanged();

                MudDialog.Close(DialogResult.Ok(ProductDetail.Id));
            }

            _loading = false;
        }

        private void Cancel()
        {
            MudDialog.Cancel();

            ProductDetail.Reference = OriginalProductDetail.Reference;
            ProductDetail.Name = OriginalProductDetail.Name;
            ProductDetail.Description = OriginalProductDetail.Description;
            ProductDetail.Height = OriginalProductDetail.Height;
            ProductDetail.TopDiameter = OriginalProductDetail.TopDiameter;
            ProductDetail.BottomDiameter = OriginalProductDetail.BottomDiameter;
            ProductDetail.Price = OriginalProductDetail.Price;
            ProductDetail.Status = OriginalProductDetail.Status;
            ProductDetail.DesignInstruction = OriginalProductDetail.DesignInstruction;
            ProductDetail.GlazingInstruction = OriginalProductDetail.GlazingInstruction;
        }
    }
}