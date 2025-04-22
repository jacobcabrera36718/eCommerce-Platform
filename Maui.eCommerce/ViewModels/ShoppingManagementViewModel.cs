using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public ItemViewModel? SelectedItem { get; set; }
        public ItemViewModel? SelectedCartItem { get; set; }

        public ObservableCollection<ItemViewModel?> Inventory
        {
            get
            {
                var items = _invSvc.Products.Where(i => i?.Stock > 0);

                if (SelectedSortOption == "Price")
                {
                    items = items.OrderBy(i => i?.Product?.Price);
                }
                else
                {
                    items = items.OrderBy(i => i?.Product?.Name);
                }
                return new ObservableCollection<ItemViewModel?>(_invSvc.Products.Where(i => i?.Stock > 0).Select(m => new ItemViewModel(m)));
            }
        }

        public ObservableCollection<ItemViewModel?> ShoppingCart
        {
            get
            {
                var items = _cartSvc.CartItems.Where(i => i?.Stock > 0);

                if (SelectedSortOption == "Price")
                {
                    items = items.OrderBy(i => i?.Product?.Price);
                }
                else
                {
                    items = items.OrderBy(i => i?.Product?.Name);
                }
                return new ObservableCollection<ItemViewModel?>(_cartSvc.CartItems.Where(i => i?.Stock > 0).Select(m => new ItemViewModel(m)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void PurchaseItem()
        {
            if (SelectedItem != null) 
            {
                var shouldRefresh = SelectedItem.Model.Stock >= 1;
                var updatedItem = _cartSvc.PurchaseItem(SelectedItem.Model);

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void ReturnItem()
        {
            if(SelectedItem != null)
            {
                if (SelectedCartItem != null)
                {
                    var shouldRefresh = SelectedCartItem.Model.Stock >= 1;
                    var updatedItem = _cartSvc.ReturnItem(SelectedCartItem.Model);

                    if (updatedItem != null && shouldRefresh)
                    {
                        NotifyPropertyChanged(nameof(Inventory));
                        NotifyPropertyChanged(nameof(ShoppingCart));
                    }
                }
            }
        }

        public string SelectedSortOption { get; set; } = "Name";
        public List<string> SortOptions => new List<string> { "Name", "Price" };

        public void SortChanged()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }


    }
}
