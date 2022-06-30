using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.ProductDetailPage
{
    public partial class ProductDetailPageComposition
    {
        [Parameter] public Product ProductDetail { get; set; } = new();
    }
}
