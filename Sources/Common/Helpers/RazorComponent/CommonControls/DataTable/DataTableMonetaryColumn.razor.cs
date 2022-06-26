using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTableMonetaryColumn<TItem> : DataTableColumn<TItem>
    {
        public override MarkupString ShowResult(TItem item)
        {
            var dd = Field.Compile();
            double? result = (double?)dd(item);

            if (!result.HasValue)
                return (MarkupString)"--";

            return (MarkupString)string.Format("{0:C}", result);
        }
    }
}