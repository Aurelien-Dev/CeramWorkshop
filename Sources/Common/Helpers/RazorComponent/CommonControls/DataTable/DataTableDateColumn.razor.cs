using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTableDateColumn<TItem> : DataTableColumn<TItem>
    {
        public override MarkupString ShowResult(TItem item)
        {
            var dd = Field.Compile();
            DateTime result = (DateTime)dd(item);

            return (MarkupString)result.ToShortDateString();
        }
    }
}