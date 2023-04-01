using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Client.Pages.FiringDetailPage.Dialogs
{
    public partial class FiringDialog : CustomComponentBase
    {
        [Inject] private IProductWorker productWorker { get; set; } = default!;
        [Inject] public IStringLocalizer<Translations> Localizer { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public Firing FiringlDetail { get; set; } = new();
        [Parameter] public bool? InsertMode { get; set; } = new();

        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();


        private async Task OnValidSubmit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                if (InsertMode.HasValue && InsertMode.Value)
                    await productWorker.FiringRepository.Add(FiringlDetail);
                else
                    productWorker.FiringRepository.Update(FiringlDetail);

                productWorker.Completed();

                StateHasChanged();
                MudDialog.Close(DialogResult.Ok(FiringlDetail));
            }
        }

        void Cancel()
        {
            productWorker.Rollback();
            StateHasChanged();
            MudDialog.Cancel();
        }
    }
}