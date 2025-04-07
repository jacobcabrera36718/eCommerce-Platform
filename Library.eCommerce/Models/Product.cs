using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_Platform.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public Product()
        {
            Name = string.Empty;
        }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}";
            } 
        }

        public Product(Product p)
        {
            Name = p.Name;
            Id = p.Id;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
