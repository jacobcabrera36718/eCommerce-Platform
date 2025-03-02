using eCommerce_Platform.Models;
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ProductDetailView : ContentPage
{
	public ProductDetailView()
	{
		InitializeComponent();
		BindingContext = new ProductViewModel();
	}

    private void Add_Return_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void Submit_Clicked(object sender, EventArgs e)
    {
        var name = (BindingContext as ProductViewModel).Name;
        ProductServiceProxy.Current.AddOrUpdate(new Product { Name = name });
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}