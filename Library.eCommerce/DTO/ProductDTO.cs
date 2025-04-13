using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce_Platform.Models;

namespace Library.eCommerce.DTO
{
    public class ProductDTO
    {
        public decimal Price { get; set; }

        public int? Id { get; set; }
        public string? Name { get; set; }


        public ProductDTO()
        {
            Name = string.Empty;
            Price = 0m;
        }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}:  {Price:C}";
            }
        }

        public ProductDTO(Product p)
        {
            Name = p.Name;
            Id = p.Id;
            Price = p.Price;
        }

        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
            Price = p.Price;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
