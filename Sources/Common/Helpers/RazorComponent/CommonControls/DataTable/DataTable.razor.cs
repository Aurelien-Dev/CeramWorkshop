using Microsoft.AspNetCore.Components;

namespace Common.Helpers.RazorComponent.CommonControls
{
    public partial class DataTable<TItem> : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; } = default!;
        [Parameter] public ICollection<TItem> Datas { get; set; }
        [Parameter] public string Id { get; set; } = default!;

        List<DataTableColumn<TItem>> Columns { get; set; } = new List<DataTableColumn<TItem>>();

        internal void AddColumn(DataTableColumn<TItem> dataTableColumn)
        {
            Columns.Add(dataTableColumn);
        }
    }
}
