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

    private async void Checkout_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CheckoutDetailView());
    }

    private void GoToCheckout_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("CheckoutDetailView");
    }

    private void CheckoutPage_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("CheckoutDetailView");
    }

    private void InlineAdd_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshProductList();
    }
}