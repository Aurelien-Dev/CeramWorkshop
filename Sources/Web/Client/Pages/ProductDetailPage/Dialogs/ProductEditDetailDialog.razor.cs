using Client.Utils;
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
        [Parameter] public bool? InsertMode { get; set; } = new();

        private MudForm _form = new();
        private bool _success;
        private string[] _errors = Array.Empty<string>();
        private bool _loading = false;
        
        private async Task OnValidSubmit()
        {
            _loading = true;
            StateHasChanged();
            await _form.Validate();

            if (_form.IsValid)
            {
                await Task.Delay(550);
                ProductDetail.IdWorkshop = CurrentSession.Workshop.Id;
                if (InsertMode.HasValue && InsertMode.Value)
                    await ProductWorker.ProductRepository.Add(ProductDetail);
                else
                    ProductWorker.ProductRepository.Update(ProductDetail);

                StateHasChanged();
                await ProductWorker.Completed();

                MudDialog.Close(DialogResult.Ok(ProductDetail.Id));
            }

            _loading = false;
        }

        private void Cancel()
        {
            ProductWorker.Rollback();
            MudDialog.Cancel();
        }

        public override  void Dispose()
        {
            ProductWorker.Close();
        }
    }
}