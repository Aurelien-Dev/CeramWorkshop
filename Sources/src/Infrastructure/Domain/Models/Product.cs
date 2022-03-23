using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? DesignInstruction { get; set; }
        public int? Status { get; set; }
    }
}
