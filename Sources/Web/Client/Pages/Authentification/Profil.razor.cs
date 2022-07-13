using Client.Services;
using Client.Utils;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Profil : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;
        [Inject] public IDataProtectionProvider dataProtectionProvider { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        public RegisterInfo registerInfo { get; set; } = new();

        Workshop currentWorkshop;
        MudForm formGeneral = new();
        MudForm formPassword = new();
        string registerError = string.Empty;

        protected override void OnInitialized()
        {
            currentWorkshop = CurrentSession.Workshop;

            registerInfo.Email = currentWorkshop.Email;
            registerInfo.UserName = currentWorkshop.UserName;
            registerInfo.Name = currentWorkshop.Name;
        }

        public async Task EditProfile()
        {
            await formGeneral.Validate();
            
            if (!formGeneral.IsValid) return;

            Workshop workshop = await worker.WorkshopRepository.Get(currentWorkshop.Id);
            workshop.Name = registerInfo.Name;
            workshop.Email = registerInfo.Email;
            workshop.UserName = registerInfo.UserName;

            worker.WorkshopRepository.Update(workshop);
            worker.Completed();

            Snackbar.Add("Modification effectuée", Severity.Success);
        }

        public async Task EditPassword()
        {
            await formPassword.Validate();

            if (!formPassword.IsValid) return;
        }

    }
}