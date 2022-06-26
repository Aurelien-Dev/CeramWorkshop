using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTableBoolColumn<TItem> : DataTableColumn<TItem>
    {
        public override MarkupString ShowResult(TItem item)
        {
            var dd = Field.Compile();
            bool boolResult = (bool)dd(item);

            if (boolResult)
                return (MarkupString)"Oui";
            else
                return (MarkupString)"Non";
        }
    }
}