using Client.Pages.ProductDetailPage.Dialogs;
using Client.Utils;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Shared
{
    public partial class MainLayout : PageLayoutComponentBase
    {
        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        public async Task NewProduct()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters { ["ProductDetail"] = new Product(), ["InsertMode"] = true };

            var dialog = DialogService.Show<ProductEditDetail>("Nouveau produit", parameters, options);

            var result = await dialog.Result;

            if (result.Cancelled) return;

            NavigationManager.NavigateTo($"/Product/{result.Data}", true);
        }
    }
}
