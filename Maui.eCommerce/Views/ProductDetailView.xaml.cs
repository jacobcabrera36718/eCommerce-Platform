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
}