using System.Xml.Serialization;
using eCommerce_Platform.Models;
using Library.eCommerce.Services;


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
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine(),
                        });
                        break;
                    case '2':
                        //update case
                        //select product
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProduct = list.FirstOrDefault(p => p.Id == selection);
                        if(selectedProduct != null)
                        {
                            selectedProduct.Name = Console.ReadLine() ?? "Error";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProduct);

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
                    case '0':
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
