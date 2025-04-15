using System.Diagnostics;
using eCommerce_Platform.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.eCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item?> Get()
        {
            return new List<Item?>
            {
                new Item{Product = new ProductDTO{Id = 1, Name = "Product 1", Price = 9.99m}, Id = 1, Stock = 1},
                new Item{Product = new ProductDTO{Id = 2, Name = "Product 2", Price = 19.99m}, Id = 2, Stock = 2},
                new Item{Product = new ProductDTO{Id = 3, Name = "Product 3", Price = 2m}, Id = 3, Stock = 3}
            };

            //return new List<Product>
            //{
            //    new Product { Id = 1, Name = "Something 1" }
            //    ,
            //    new Product { Id = 2, Name = "Something 2" }
            //    ,
            //    new Product { Id = 3, Name = "Something 3" }
            //};
        }
    }
}
