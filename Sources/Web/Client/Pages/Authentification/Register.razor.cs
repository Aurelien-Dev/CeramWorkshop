using Client.Services;
using Client.Utils.ComponentBase;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Register : CustomComponentBase
    {
        [Inject] public IWorkshopWorker WorkshopWorker { get; set; } = default!;

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

                if (await WorkshopWorker.WorkshopRepository.CheckIfEmailExists(_registerInfo.Email, ComponentDisposed))
                {
                    _registerError = "Email already in use.";
                    return;
                }

                var workshopSalt = ProtectedDataService.GetSalt();
                var workshopPasswordHash = ProtectedDataService.HashPassword(_registerInfo.Password, workshopSalt);

                Workshop workshopDetail = new(_registerInfo.Name, null, _registerInfo.Email, _registerInfo.UserName, workshopPasswordHash, workshopSalt);

                await WorkshopWorker.WorkshopRepository.Add(workshopDetail, ComponentDisposed);
                await WorkshopWorker.Completed();
                
                (bool success, _registerError) = await AuthenticationManager.StartSession(workshopDetail.Email, _registerInfo.Password);

                if (!success) return;

                NavigationManager.NavigateTo("/", forceLoad: false);
            }
        }
    }
}