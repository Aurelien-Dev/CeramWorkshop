using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.ProductDetailPage.Dialogs
{
    public partial class ProductEditDetailDialog : CustomComponentBase
    {
        [Inject] private IProductWorker ProductWorker { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Product ProductDetail { get; set; } = default!;
        [Parameter] public bool? InsertMode { get; set; } = new();

        MudForm _form = new();
        bool _success;
        string[] _errors = Array.Empty<string>();
        bool _loading = false;


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
                ProductWorker.Completed();

                MudDialog.Close(DialogResult.Ok<int>(ProductDetail.Id));
            }

            _loading = false;
        }

        private void Cancel()
        {
            ProductWorker.Rollback();
            MudDialog.Cancel();
        }
    }
}