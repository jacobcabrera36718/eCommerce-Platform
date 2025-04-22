using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eCommerce_Platform.Models;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        private Item? selectedProduct {  get; set; }
        public string? Query {  get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;
        public event PropertyChangedEventHandler? PropertyChanged;


        public Item? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged();

            }
        }
        

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
            NotifyPropertyChanged(nameof(Products));
        }


        public ObservableCollection<Item?> Products 
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? String.Empty) ?? false);
                if (SelectedSortOption == "Price")
                {
                    filteredList = filteredList.OrderBy(p => p?.Product?.Price);
                }
                else
                {
                    filteredList = filteredList.OrderBy(p => p?.Product?.Name);
                }
                return new ObservableCollection<Item?>(filteredList);
            }
        }

        public async Task<bool> Search()
        {
            await _svc.Search(Query);
            NotifyPropertyChanged(nameof(Products));
            return true;
        }

        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }

        public string SelectedSortOption { get; set; } = "Name"; // Default

        public List<string> SortOptions => new List<string> { "Name", "Price" };

        public void SortChanged()
        {
            NotifyPropertyChanged(nameof(Products));
        }
    }
}
