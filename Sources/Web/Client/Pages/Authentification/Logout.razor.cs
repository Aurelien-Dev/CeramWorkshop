using Client.Utils;
using Client.Utils.ComponentBase;
using Microsoft.AspNetCore.Authorization;

namespace Client.Pages.Authentification
{
    [Authorize]
    public partial class Logout : CustomLayoutComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await AuthenticationManager.StopSession();

            NavigationManager.NavigateTo("/", true);
        }
    }
}