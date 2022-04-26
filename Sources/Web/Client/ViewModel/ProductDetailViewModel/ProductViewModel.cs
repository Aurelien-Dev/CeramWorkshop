using Domain.Models;

namespace Client.ViewModel.ProductDetailViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Name { get; set; }
        public int CountImg { get; set; }
        public ProductStatus? Status { get; set; }
        public string StatusText { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DesignInstruction { get; set; }
        public string Height { get; set; }
        public string TopDiameter { get; set; }
        public string BottomDiameter { get; set; }

        public ProductViewModel()
        {

        }
        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Reference = product.Reference;
            Name = product.Name;
            Status = product.Status;
            Description = product.Description;
            DesignInstruction = product.DesignInstruction;

            if (product.Height.HasValue) Height = product.Height.Value.ToString();
            if (product.TopDiameter.HasValue) TopDiameter = product.TopDiameter.Value.ToString();
            if (product.BottomDiameter.HasValue) BottomDiameter = product.BottomDiameter.Value.ToString();

            if (Status.HasValue) StatusText = Status.Value.ToString();

        }
    }
}
