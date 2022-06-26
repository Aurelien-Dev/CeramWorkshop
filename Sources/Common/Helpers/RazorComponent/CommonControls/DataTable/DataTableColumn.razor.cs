using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTableColumn<TItem>
    {
        [CascadingParameter] public DataTable<TItem> DataTable { get; set; }
        [Parameter] public string Name { get; set; } = default!;
        [Parameter] public Expression<Func<TItem, object>> Field { get; set; } = default!;

        public virtual MarkupString ShowResult(TItem item)
        {
            var dataCompile = Field.Compile();
            return (MarkupString)dataCompile(item)?.ToString();
        }

        protected override void OnInitialized()
        {
            DataTable?.AddColumn(this);
        }
    }
}
