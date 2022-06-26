using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTableTextColumn<TItem> : DataTableColumn<TItem>
    {
        public override MarkupString ShowResult(TItem item)
        {
            return base.ShowResult(item);
        }
    }
}