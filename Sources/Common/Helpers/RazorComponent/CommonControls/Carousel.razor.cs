using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class Carousel : PageComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    }
}