using Common.Helpers.RazorComponent;

namespace Client.Shared
{
    public partial class MainLayout : PageLayoutComponentBase
    {
        protected async override void OnAfterRender(bool firstRender)
        {
            //_ = await JSRuntime.InvokeAsync<string>("initMain", null);
        }
    }
}