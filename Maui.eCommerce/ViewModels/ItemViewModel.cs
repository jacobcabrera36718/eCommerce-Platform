using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ItemViewModel
    {
        public int StockToAdd { get; set; } = 1;

        public Item Model { get; set; }
         public ICommand? AddCommand { get; set; }
        private void DoAdd()
        {
            for (int i = 0; i < StockToAdd; i++)
            {
                ShoppingCartService.Current.AddOrUpdate(Model);
            }
        }

        void SetUpCommands()
        {
            AddCommand = new Command(DoAdd);
        }
        public ItemViewModel()
        {
            Model = new Item();
            SetUpCommands();
        }
        public ItemViewModel(Item model)
        {
            Model = model;
            SetUpCommands();
        }
    }
}
