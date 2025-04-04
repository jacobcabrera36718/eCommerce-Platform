using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce_Platform.Models;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        private Product? cashedModel {  get; set; }
        public string? Name { 
            get
            {
                return Model?.Product?.Name ?? string.Empty;
            }
            set
            {
                if (Model != null && Model.Product?.Name != value)
                {
                    Model.Product.Name = value;
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

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
           ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public ProductViewModel()
        {
            Model = new Item();
            cashedModel = null;
        }

        public ProductViewModel(Item? model)
        {
            Model = model;
        }
    }
}
