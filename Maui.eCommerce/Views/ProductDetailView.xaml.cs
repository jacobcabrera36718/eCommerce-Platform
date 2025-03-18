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
        var vm = BindingContext as ProductViewModel;
        if (vm != null)
        {
            var product = new Product
            {
                Id = vm.Model?.Id ?? 0, // For edits, pass along the ID
                Name = vm.Name,
                Stock = vm.Stock ?? 0
            };

            ProductServiceProxy.Current.AddOrUpdate(product);
            Shell.Current.GoToAsync("//InventoryManagement");
        }
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