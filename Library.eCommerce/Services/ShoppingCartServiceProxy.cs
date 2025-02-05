using System;
using System.Collections.Generic;
using System.Linq;
using eCommerce_Platform.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartServiceProxy
    {
        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();
        private Dictionary<int, int> cartItems; 

        private ShoppingCartServiceProxy()
        {
            cartItems = new Dictionary<int, int>();
        }

        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }
                return instance;
            }
        }

        public void AddToCart(int productId, int quantity)
        {
            var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                if (product.Stock >= quantity)
                {
                    product.Stock -= quantity;  

                    if (cartItems.ContainsKey(productId))
                    {
                        cartItems[productId] += quantity;
                    }
                    else
                    {
                        cartItems[productId] = quantity;
                    }

                    Console.WriteLine($"Added {quantity} of {product.Name} to cart.");
                }
                else
                {
                    Console.WriteLine($"Not enough stock available. Only {product.Stock} left.");
                }
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }


        public void RemoveFromCart(int productId, int quantity)
        {
            if (cartItems.ContainsKey(productId))
            {
                if (cartItems[productId] > quantity)
                {
                    cartItems[productId] -= quantity;
                }
                else
                {
                    cartItems.Remove(productId);
                }

                var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    product.Stock += quantity;  
                }

                Console.WriteLine($"Returned {quantity} of Product ID {productId} to inventory.");
            }
            else
            {
                Console.WriteLine("Product not in cart.");
            }
        }


        public void ViewCart()
        {
            if (cartItems.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            Console.WriteLine("\n--- Shopping Cart ---");
            Console.WriteLine("ID | Product Name        | Quantity");
            Console.WriteLine("----------------------------------");

            foreach (var item in cartItems)
            {
                var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == item.Key);
                if (product != null)
                {
                    Console.WriteLine($"{product.Id}  | {product.Name,-18} | {item.Value}");
                }
            }
        }


        public void Checkout()
        {
            if (cartItems.Count == 0)
            {
                Console.WriteLine("Your cart is empty. Nothing to checkout.");
                return;
            }

            decimal total = 0;
            foreach (var item in cartItems)
            {
                var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == item.Key);
                if (product != null)
                {
                    decimal price = 10.0m; 
                    decimal cost = item.Value * price;
                    total += cost;
                }
            }

            decimal tax = total * 0.07m;
            decimal finalTotal = total + tax;

            Console.WriteLine("\n----- Receipt -----");
            Console.WriteLine($"Subtotal: ${total:F2}");
            Console.WriteLine($"Tax (7%): ${tax:F2}");
            Console.WriteLine($"Total: ${finalTotal:F2}");
            Console.WriteLine("Thank you for your purchase!");

            cartItems.Clear();
        }

    }
}
