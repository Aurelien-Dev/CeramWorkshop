using Client.Pages.ProductDetailPage.Dialogs;
using Client.Utils;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Client.Shared
{
    public partial class MainLayout : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;

        bool _drawerOpen = true;


        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        public async Task NewProduct()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["ProductDetail"] = new Product(), ["InsertMode"] = true };

            var dialog = DialogService.Show<ProductEditDetailDialog>("Nouveau produit", parameters, options);

            var result = await dialog.Result;

            if (result.Canceled) return;

            NavigationManager.NavigateTo($"/Product/{result.Data}", true);
        }
    }
}