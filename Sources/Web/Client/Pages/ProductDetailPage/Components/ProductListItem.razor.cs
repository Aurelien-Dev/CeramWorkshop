using Client.Pages.ProductDetailPage.Components.ViewModel;
using Client.Utils;
using Client.Utils.ComponentBase;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage.Components;

public partial class ProductListItem : CustomComponentBase
{
    [Parameter] public ProductListItemViewModel ProductListItemVm { get; set; } = default!;
}

