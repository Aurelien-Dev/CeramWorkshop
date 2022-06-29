using Client.Utils;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.MaterialDetailPage
{
    public partial class ListMaterialPage : PageComponentBase
    {
        [Inject] private IMaterialRepository MaterialRepository { get; set; } = default!;

        public ICollection<Material> Materials { get; set; } = new List<Material>();

        protected override async Task OnInitializedAsync()
        {
            Materials = await MaterialRepository.GetAll();
        }
    }
}