using Client.Utils;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Profil : CustomLayoutComponentBase
    {
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        public RegisterInfo registerInfo { get; set; } = new();

        MudForm formGeneral = new();
        MudForm formPassword = new();
        string registerError = string.Empty;

        protected override void OnInitialized()
        {
            registerInfo.Email = CurrentSession.Workshop.Email;
            registerInfo.UserName = CurrentSession.Workshop.UserName;
            registerInfo.Name = CurrentSession.Workshop.Name;
            registerInfo.Culture = new RequestCulture(CurrentSession.Workshop.Culture);
        }

        public async Task EditProfile()
        {
            await formGeneral.Validate();

            if (!formGeneral.IsValid) return;

            Workshop workshop = await worker.WorkshopRepository.Get(CurrentSession.Workshop.Id);
            workshop.Name = registerInfo.Name;
            workshop.Email = registerInfo.Email;
            workshop.UserName = registerInfo.UserName;
            workshop.Culture = registerInfo.Culture.Culture.Name;

            CurrentSession.Workshop.Culture = registerInfo.Culture.Culture.Name;

            await JSRuntime.InvokeAsync<string>("setCookie", new object[]
               {
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(registerInfo.Culture), 365
               });

            worker.WorkshopRepository.Update(workshop);
            worker.Completed();

            Snackbar.Add("Modification effectuée", Severity.Success);

            await Task.Delay(500);
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }

        public async Task EditPassword()
        {
            await formPassword.Validate();

            if (!formPassword.IsValid) return;
        }
    }
}