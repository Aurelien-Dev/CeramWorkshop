﻿using Domain.CustomDataAnotation;
using Domain.Models.WorkshopDomaine;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.MainDomain
{
    public enum ProductStatus { None = 0, Test = 1, Abandoned = 2, Production = 3, Discontinued = 4 }

    public class Product
    {
        [CeramRequired]
        public int Id { get; set; }

        [CeramRequired]
        public int IdWorkshop { get; set; }

        [CeramRequired]
        public string Reference { get; set; }

        [CeramRequired]
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Height { get; set; }
        public double? TopDiameter { get; set; }
        public double? BottomDiameter { get; set; }
        public double? ShrinkingCoeficient { get; set; }
        public string? DesignInstruction { get; set; }
        public string? GlazingInstruction { get; set; }
        public ProductStatus Status { get; set; }

        public ICollection<ImageInstruction> ImageInstructions { get; set; } = new List<ImageInstruction>();
        public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();
        public Workshop Workshop { get; set; } = default!;

        public Product() { }

        public Product(string name, string reference)
        {
            Name = name;
            Reference = reference;
        }
    }
}