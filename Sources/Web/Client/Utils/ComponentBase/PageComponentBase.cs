using Microsoft.AspNetCore.Components;

namespace Client.Utils
{
    public enum PageMode { New, Edit, Show }

    public abstract class PageComponentBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    }
}