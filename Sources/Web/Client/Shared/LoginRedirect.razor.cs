using Client.Utils;

namespace Client.Shared
{
    public partial class LoginRedirect : CustomComponentBase
    {
        protected override void OnAfterRender(bool firstRender)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }
}
