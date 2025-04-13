using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce_Platform.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Item?>
            {
                new Item{Product = new ProductDTO{Id = 1, Name = "Product 1", Price = 9.99m}, Id = 1, Stock = 1},
                new Item{Product = new ProductDTO{Id = 2, Name = "Product 2", Price = 19.99m}, Id = 2, Stock = 2},
                new Item{Product = new ProductDTO{Id = 3, Name = "Product 3", Price = 15.99m}, Id = 3, Stock = 3}
            };
        }

        private int lastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }
                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Item?> Products { get; private set; }

        public Item AddOrUpdate(Item item)
        {
            if (item.Id == 0)
            {
                item.Id = lastKey + 1;
                item.Product.Id = item.Id;
                Products.Add(item);
            }
            else
            {
                var existingItem = Products.FirstOrDefault(p => p.Id == item.Id);
                var index = Products.IndexOf(existingItem);
                Products.RemoveAt(index);
                Products.Insert(index, new Item(item));
            }

            return item;
        }

       

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            Item? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);

            return product;
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
