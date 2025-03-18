using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce_Platform.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name { 
            get
            {
                return Model?.Name ?? string.Empty;
            }
            set
            {
                if (Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public int? Stock
        {
            get => Model?.Stock;
            set
            {
                if (Model != null && Model.Stock != value)
                {
                    Model.Stock = value ?? 0;
                }
            }
        }

        public Product? Model { get; set; }

        public void AddOrUpdate()
        {
            var product = new Product
            {
                Name = Model?.Name,
                Stock = Model?.Stock ?? 0,
            };

            ProductServiceProxy.Current.AddOrUpdate(product);
        }

        public ProductViewModel()
        {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = model;
        }
    }
}
