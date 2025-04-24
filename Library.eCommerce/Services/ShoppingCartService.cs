using System;
using System.Collections.Generic;
using System.Linq;
using eCommerce_Platform.Models;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;

        // Multi-cart/wishlist implementation
        private Dictionary<string, List<Item>> carts = new()
        {
            { "Default", new List<Item>() },
        };
        public List<string> CartNames => carts.Keys.ToList();
        public string SelectedCartName { get; set; } = "Default";
        public List<Item> CartItems => carts[SelectedCartName];

        public static ShoppingCartService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }
                return instance;
            }
        }
        private static ShoppingCartService? instance;

        private ShoppingCartService()
        {
        }

        public Item? PurchaseItem(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Stock == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Stock--;
            }

            var currentCart = carts[SelectedCartName];
            var existingItem = currentCart.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Stock = 1;
                currentCart.Add(newItem);
            }
            else
            {
                existingItem.Stock++;
            }

            return existingInvItem;
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Stock == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Stock--;
            }

            var currentCart = carts[SelectedCartName];
            var existingItem = currentCart.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Stock = 1;
                currentCart.Add(newItem);
            }
            else
            {
                existingItem.Stock++;
            }
            return existingInvItem;
        }

        public Item? ReturnItem(Item item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }

            var currentCart = carts[SelectedCartName];
            var itemToReturn = currentCart.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Stock--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Stock++;
                }
            }

            return itemToReturn;
        }

        public string AddNewWishlist()
        {
            int index = 1;
            string name;
            do
            {
                name = $"Wishlist {index}";
                index++;
            } while (carts.ContainsKey(name));
            carts.Add(name, new List<Item>());
            return name;
        }

    }
}
