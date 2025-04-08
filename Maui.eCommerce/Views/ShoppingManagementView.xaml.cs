using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    private void AddToCart_Clicked(object sender, EventArgs e)
    {
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

    private void RemoveFromCart_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }
}