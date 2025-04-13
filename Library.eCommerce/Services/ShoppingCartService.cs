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
        private List<Item> items;
        public List<Item> CartItems
        {
            get
            {
                return items;
            }
        }
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
            items = new List<Item>();
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

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Stock = 1;
                CartItems.Add(newItem);
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

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                //add
                var newItem = new Item(item);
                newItem.Stock = 1;
                CartItems.Add(newItem);
            }
            else
            {
                //update
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

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Stock--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if(inventoryItem == null)
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
    }
}
