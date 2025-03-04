using eCommerce_Platform.Models;
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetailView : ContentPage
{
	public ProductDetailView()
	{
		InitializeComponent();  
    }

    public int ProductId { get; set; }

    private void Add_Return_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void Submit_Clicked(object sender, EventArgs e)
    {
        var name = (BindingContext as ProductViewModel)?.Name;
        var stock = (BindingContext as ProductViewModel)?.Stock;
        ProductServiceProxy.Current.AddOrUpdate(new Product { Name = name, Stock = stock ?? 0});
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (ProductId == 0) 
        {
            BindingContext = new ProductViewModel();
        }
        else
        {
            BindingContext = new ProductViewModel(ProductServiceProxy.Current.GetById(ProductId));
        }
    }

}