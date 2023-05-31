using Domain.Models.MainDomain;

namespace Client.Pages.ProductDetailPage.Components.ViewModel;

public class ProductListItemViewModel
{
    public int Id { get; set; }
    public string? Reference { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public ProductStatus Status { get; set; }

    public ProductListItemViewModel(Product? product)
    {
        Id = product.Id;
        Reference = product.Reference;
        Name = product.Name;
        Status = product.Status;
        if (product.FavoriteImage != null)
            Image = product.FavoriteImage.UrlSmall;
    }
}