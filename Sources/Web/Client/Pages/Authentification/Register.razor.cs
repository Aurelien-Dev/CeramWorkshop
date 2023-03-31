using Client.Services;
using Client.Utils;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Register : CustomLayoutComponentBase
    {
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        RegisterInfo registerInfo = new();

        MudForm form = new();
        bool validForm;
        string registerError = string.Empty;
        public bool RegisterInProgress { get; set; } = false;

        private async Task RegisterWorkshop()
        {
            registerError = string.Empty;
            await Task.Delay(5);

            await form.Validate();

            StateHasChanged();
            if (form.IsValid)
            {
                RegisterInProgress = true;
                StateHasChanged();

                if (worker.WorkshopRepository.CheckIfEmailExists(registerInfo.Email))
                {
                    registerError = "Email already in use.";
                    return;
                }

                var WorkshopSalt = ProtectedDataService.GetSalt();
                var WorkshopPasswordHash = ProtectedDataService.HashPassword(registerInfo.Password, WorkshopSalt);

                Workshop workshopDetail = new(registerInfo.Name, null, registerInfo.Email, registerInfo.UserName, WorkshopPasswordHash, WorkshopSalt);

                await worker.WorkshopRepository.Add(workshopDetail);
                worker.Completed();

                (bool success, registerError) = await AuthenticationManager.StartSession(workshopDetail.Email, registerInfo.Password);

                if (!success) return;

                NavigationManager.NavigateTo("/", forceLoad: false);
            }
        }
    }
}