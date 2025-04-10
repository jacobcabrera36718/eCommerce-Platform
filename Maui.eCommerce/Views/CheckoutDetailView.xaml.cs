using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System.Collections.ObjectModel;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutDetailView : ContentPage
{
    public ObservableCollection<Item?> CartItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalWithTax { get; set; }

    public CheckoutDetailView()
    {
        InitializeComponent();

        CartItems = new ObservableCollection<Item?>(
            ShoppingCartService.Current.CartItems.Where(i => i?.Stock > 0));

        Subtotal = CartItems.Sum(item => (item?.Stock ?? 0) * (item?.Product?.Price ?? 0m));
        Tax = Subtotal * 0.07m;
        TotalWithTax = Subtotal + Tax;

        BindingContext = this;
    }
}
