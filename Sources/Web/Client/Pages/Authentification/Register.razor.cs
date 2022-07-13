using Client.Services;
using Client.Utils;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Register : CustomLayoutComponentBase
    {
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        RegisterInfo registerInfo = new();

        MudForm form = new();
        bool success;
        string registerError = string.Empty;

        private async Task RegisterWorkshop()
        {
            registerError = string.Empty;
            await Task.Delay(5);

            StateHasChanged();
            await form.Validate();

            if (form.IsValid)
            {
                if(worker.WorkshopRepository.CheckIfEmailExists(registerInfo.Email))
                {
                    registerError = "Email already in use.";
                    return;
                }

                var WorkshopSalt = ProtectedDataService.GetSalt();
                var WorkshopPasswordHash = ProtectedDataService.HashPassword(registerInfo.Password, WorkshopSalt);

                Workshop workshopDetail = new(registerInfo.Name, null, registerInfo.Email, registerInfo.UserName, WorkshopPasswordHash, WorkshopSalt);

                await worker.WorkshopRepository.Add(workshopDetail);
                worker.Completed();

                registerError = await AuthenticationManager.StartSession(workshopDetail.Email, workshopDetail.PasswordHash);
                if (!string.IsNullOrEmpty(registerError)) return;

                NavigationManager.NavigateTo("/", forceLoad: false);
            }
        }
    }
}