using Client.Utils.ComponentBase;
using Client.ViewModels;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Profil : CustomComponentBase
    {
        [Inject] public IWorkshopWorker WorkshopWorker { get; set; } = default!;

        private RegisterInfo RegisterInfo { get; set; } = new();
        private MudForm _formGeneral = new();

        protected override void OnInitialized()
        {
            RegisterInfo.Email = CurrentSession.Workshop.Email;
            RegisterInfo.UserName = CurrentSession.Workshop.UserName;
            RegisterInfo.Name = CurrentSession.Workshop.Name;
            RegisterInfo.Culture = new RequestCulture(CurrentSession.Workshop.Culture);
        }

        private async Task EditProfile()
        {
            await _formGeneral.Validate();

            if (!_formGeneral.IsValid) return;

            Workshop workshop = await WorkshopWorker.WorkshopRepository.Get(CurrentSession.Workshop.Id, ComponentDisposed);
            workshop.Name = RegisterInfo.Name;
            workshop.Email = RegisterInfo.Email;
            workshop.UserName = RegisterInfo.UserName;
            workshop.Culture = RegisterInfo.Culture.Culture.Name;

            CurrentSession.Workshop.Culture = RegisterInfo.Culture.Culture.Name;

            await JsRuntime.InvokeAsync<string>("setCookie", new object[]
            {
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(RegisterInfo.Culture), 365
            });

            WorkshopWorker.WorkshopRepository.Update(workshop);
            await WorkshopWorker.Completed();
            
            Snackbar.Add("Modification effectu�e", Severity.Success);

            await Task.Delay(500);
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }
    }
}