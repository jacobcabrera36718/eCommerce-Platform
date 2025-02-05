using System.Xml.Serialization;
using eCommerce_Platform.Models;
using Library.eCommerce.Services;
using Library.eCommerce.ShoppingCart;


namespace eCommerce_Platform
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //provide user menu
            Console.WriteLine("Welcome to my Shop");
            Console.WriteLine("1. Create new Inventory Item");
            Console.WriteLine("2. Update an Inventory Item");
            Console.WriteLine("3. Read all the Inventory Items");
            Console.WriteLine("4. Delete an Inventory Item");
            Console.WriteLine("5. Add item to cart");
            Console.WriteLine("6. Remove item from cart");
            Console.WriteLine("7. View cart");
            Console.WriteLine("8. Checkout");
            Console.WriteLine("0. Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;

            char choice;

            do
            {

                //take in user input 
                string? input = Console.ReadLine();
                choice = input[0];

                //switch case
                switch (choice)
                {
                    case '1':
                        //create case
                        //add to list
                        Console.Write("Enter product name: ");
                        string productName = Console.ReadLine() ?? "Unnamed Product";

                        Console.Write("Enter initial stock quantity: ");
                        int stockQuantity = int.Parse(Console.ReadLine() ?? "0");

                        // Find the next available ID
                        int newId = (ProductServiceProxy.Current.Products.Count > 0)
                            ? ProductServiceProxy.Current.Products.Max(p => p.Id ?? 0) + 1
                            : 1;

                        ProductServiceProxy.Current.Products.Add(new Product
                        {
                            Id = newId, 
                            Name = productName,
                            Stock = stockQuantity
                        });
                        break;
                    case '2':
                        //update case
                        //select product
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProduct = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == selection);
                        if (selectedProduct != null)
                        {
                            Console.Write("Enter new product name: ");
                            selectedProduct.Name = Console.ReadLine() ?? "Error";

                            Console.Write("Enter new stock quantity: ");
                            selectedProduct.Stock = int.Parse(Console.ReadLine() ?? "0"); 
                        }
                        //replace with new product
                        break;
                    case '3':
                        //read case
                        //print out all the products
                        list.ForEach(Console.WriteLine);
                        break;
                    case '4':
                        //delete case
                        //select and delete product from list
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;
                    case '5':
                        //add item to cart
                        Console.Write("Enter product ID to add to cart: ");
                        int productId = int.Parse(Console.ReadLine() ?? "-1");
                        Console.Write("Enter quantity: ");
                        int quantity = int.Parse(Console.ReadLine() ?? "0");
                        ShoppingCartServiceProxy.Current.AddToCart(productId, quantity);
                        break;
                    case '6':
                        //remove item from cart
                        Console.Write("Enter product ID to remove from cart: ");
                        productId = int.Parse(Console.ReadLine() ?? "-1");
                        Console.Write("Enter quantity to remove: ");
                        quantity = int.Parse(Console.ReadLine() ?? "0");
                        ShoppingCartServiceProxy.Current.RemoveFromCart(productId, quantity);
                        break;
                    case '7':
                        //view cart
                        ShoppingCartServiceProxy.Current.ViewCart();
                        break;
                    case '8':
                        //checkout
                        ShoppingCartServiceProxy.Current.Checkout();
                        break;
                    case '0':
                        Console.WriteLine("Exiting Program");
                        break;
                    default:
                        Console.WriteLine("Error: Choose one of the commands shown");
                        break;

                }
            } while (choice != '0');

        }

        //add new products function
        static void AddProduct(List<string?> list)
        {
            var newProduct = Console.ReadLine();

            list.Add(newProduct);
        }
    }
}
