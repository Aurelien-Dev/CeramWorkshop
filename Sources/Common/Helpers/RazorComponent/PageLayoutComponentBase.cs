using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Common.Helpers.RazorComponent
{
    public abstract class PageLayoutComponentBase : LayoutComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
    }
}
