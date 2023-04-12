using Client.Services;
using Client.Utils;
using Client.Utils.ComponentBase;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Register : CustomLayoutComponentBase
    {
        [Inject] public IWorkshopWorker Worker { get; set; } = default!;

        private bool RegisterInProgress { get; set; } = false;
        
        private RegisterInfo _registerInfo = new();
        private MudForm _form = new();
        private bool _validForm;
        private string _registerError = string.Empty;

        private async Task RegisterWorkshop()
        {
            _registerError = string.Empty;
            await Task.Delay(5);

            await _form.Validate();

            StateHasChanged();
            if (_form.IsValid)
            {
                RegisterInProgress = true;
                StateHasChanged();

                if (Worker.WorkshopRepository.CheckIfEmailExists(_registerInfo.Email))
                {
                    _registerError = "Email already in use.";
                    return;
                }

                var workshopSalt = ProtectedDataService.GetSalt();
                var workshopPasswordHash = ProtectedDataService.HashPassword(_registerInfo.Password, workshopSalt);

                Workshop workshopDetail = new(_registerInfo.Name, null, _registerInfo.Email, _registerInfo.UserName, workshopPasswordHash, workshopSalt);

                await Worker.WorkshopRepository.Add(workshopDetail);
                Worker.Completed();

                (bool success, _registerError) = await AuthenticationManager.StartSession(workshopDetail.Email, _registerInfo.Password);

                if (!success) return;

                NavigationManager.NavigateTo("/", forceLoad: false);
            }
        }
    }
}