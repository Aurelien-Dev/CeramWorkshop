using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class Index : ComponentBase
    {
        public Product ProductDetail { get; set; }
        public bool IsReadonly { get; set; } = false;

        public Index()
        {
            ProductDetail = new Product("Vase");
            ProductDetail.Id = 1;
            ProductDetail.Reference = "ACV1";
            ProductDetail.Description = "Vase ming fait en grès de saint amand.";
            ProductDetail.DesignInstruction = "Insctruction de construction.";
            ProductDetail.BottomDiameter = 11;
            ProductDetail.TopDiameter = 2;
            ProductDetail.Height = 35;
            ProductDetail.Status = 1;
        }

        public void ReadonlyCmd()
        {
            IsReadonly = !IsReadonly;
        }
    }
}