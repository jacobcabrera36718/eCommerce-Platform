using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Library.eCommerce.ShoppingCart
{
    public class ShoppingCart
    {
        private Dictionary<int, int> cartItems; // ProductID -> Quantity
        private ProductServiceProxy inventory;

        public ShoppingCart(ProductServiceProxy inventory)
        {
            this.inventory = inventory;
            cartItems = new Dictionary<int, int>();
        }

        public void AddToCart(int productId, int quantity)
        {
            var product = inventory.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
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
                    Console.WriteLine($"Removed {quantity} of Product ID {productId} from cart.");
                }
                else
                {
                    cartItems.Remove(productId);
                    Console.WriteLine($"Removed Product ID {productId} from cart.");
                }
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

            Console.WriteLine("Shopping Cart:");
            foreach (var item in cartItems)
            {
                var product = inventory.Products.FirstOrDefault(p => p.Id == item.Key);
                if (product != null)
                {
                    Console.WriteLine($"{product.Name} - Quantity: {item.Value}");
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

            Console.WriteLine("\n--- Receipt ---");
            decimal total = 0;
            foreach (var item in cartItems)
            {
                var product = inventory.Products.FirstOrDefault(p => p.Id == item.Key);
                if (product != null)
                {
                    decimal price = 10.0m; // Placeholder price for each product
                    decimal cost = item.Value * price;
                    Console.WriteLine($"{product.Name} - {item.Value} x ${price} = ${cost}");
                    total += cost;
                }
            }

            decimal tax = total * 0.07m;
            decimal finalTotal = total + tax;
            Console.WriteLine($"Subtotal: ${total}");
            Console.WriteLine($"Tax (7%): ${tax}");
            Console.WriteLine($"Total: ${finalTotal}");
            Console.WriteLine("Thank you for your purchase!");

            cartItems.Clear(); // Empty the cart after checkout
        }
    }
}
