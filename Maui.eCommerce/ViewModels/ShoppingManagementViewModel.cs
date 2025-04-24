using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        // Cart selection properties
        public List<string> CartNames => _cartSvc.CartNames;

        public string SelectedCartName
        {
            get => _cartSvc.SelectedCartName;
            set
            {
                if (_cartSvc.SelectedCartName != value)
                {
                    _cartSvc.SelectedCartName = value;
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public ObservableCollection<ItemViewModel?> Inventory
        {
            get
            {
                var items = _invSvc.Products.Where(i => i?.Stock > 0);

                // Sort by selected option
                if (SelectedSortOption == "Price")
                    items = items.OrderBy(i => i?.Product?.Price);
                else
                    items = items.OrderBy(i => i?.Product?.Name);

                return new ObservableCollection<ItemViewModel?>(items.Select(m => new ItemViewModel(m)));
            }
        }


        public ObservableCollection<ItemViewModel?> ShoppingCart
        {
            get
            {
                var items = _cartSvc.CartItems.Where(i => i?.Stock > 0);

                // Sort by selected option
                if (SelectedSortOption == "Price")
                    items = items.OrderBy(i => i?.Product?.Price);
                else
                    items = items.OrderBy(i => i?.Product?.Name);

                return new ObservableCollection<ItemViewModel?>(items.Select(m => new ItemViewModel(m)));
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

        public void SortChanged()
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
            if (SelectedItem != null)
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

        public ICommand AddWishlistCommand { get; }
        public ShoppingManagementViewModel()
        {
            AddWishlistCommand = new Command(AddWishlist);
        }

        private void AddWishlist()
        {
            var newName = _cartSvc.AddNewWishlist();
            NotifyPropertyChanged(nameof(CartNames));
            SelectedCartName = newName; 
        }

        public List<string> SortOptions { get; } = new List<string> { "Name", "Price" };

        private string _selectedSortOption = "Name";
        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                if (_selectedSortOption != value)
                {
                    _selectedSortOption = value;
                    NotifyPropertyChanged(nameof(SelectedSortOption));
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }


    }
}
