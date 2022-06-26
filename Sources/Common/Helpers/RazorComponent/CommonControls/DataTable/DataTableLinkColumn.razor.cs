using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTableLinkColumn<TItem> : DataTableColumn<TItem>
    {
        [Parameter] public Expression<Func<TItem, object>> TextLinkLambda { get; set; } = default!;
        [Parameter] public string TextLink { get; set; } = default!;

        public override MarkupString ShowResult(TItem item)
        {
            string labelLink = string.Empty;

            var dataField = Field.Compile();
            string resultField = (string)dataField(item);

            if (TextLinkLambda != null)
            {
                var dataTextLink = TextLinkLambda.Compile();
                labelLink = (string)dataTextLink(item);
            }
            else
            {
                labelLink = TextLink;
            }

            if (string.IsNullOrEmpty(resultField))
                return (MarkupString)labelLink;
            else
                return (MarkupString)$"<a href={resultField} target=_blank>{labelLink}</a>";
        }
    }
}