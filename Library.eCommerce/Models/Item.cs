using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using eCommerce_Platform.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Services;

namespace Library.eCommerce.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Stock {  get; set; }
        public ICommand? AddCommand { get; set; }

        public override string ToString()
        {
            return $"{Product}   Stock:{Stock}";
        }

        public string Display
        {
            get
            {
                return $"{ Product?.Display ?? string.Empty} Stock - { Stock }";
            }
        }

        private void DoAdd()
        {
            ShoppingCartService.Current.AddOrUpdate(this);  
        }

        public Item()
        {
            Product = new ProductDTO();
            Stock = 0;
            AddCommand = new Command(DoAdd);
        }

        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Stock = i.Stock;
            Id = i.Id;
            AddCommand = new Command(DoAdd);
        }
    }
}
