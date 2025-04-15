using eCommerce_Platform.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;

namespace API.eCommerce.Database
{
    public static class FakeDatabase
    {
        private static List<Item?> inventory = new List<Item?>
            {
                new Item{Product = new ProductDTO{Id = 1, Name = "Product 1 W", Price = 9.99m}, Id = 1, Stock = 1},
                new Item{Product = new ProductDTO{Id = 2, Name = "Product 2 W", Price = 19.99m}, Id = 2, Stock = 2},
                new Item{Product = new ProductDTO{Id = 3, Name = "Product 3 W", Price = 2m}, Id = 3, Stock = 3}
            };

        public static int LastKey_Item
        {
            get
            {
                if (!inventory.Any())
                {
                    return 0;
                }
                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }

        public static List<Item?> Inventory
        {
            get
            {
                return inventory;
            }
        }
    }
}
