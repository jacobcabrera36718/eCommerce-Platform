using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace eCommerce_Platform.Models
{
    public class Product
    {
        public decimal Price { get; set; } 

        public int? Id { get; set; }
        public string? Name { get; set; }


        public Product()
        {
            Name = string.Empty;
            Price = 0m;
        }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name} - {Price:C}";
            } 
        }

        public Product(Product p)
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
