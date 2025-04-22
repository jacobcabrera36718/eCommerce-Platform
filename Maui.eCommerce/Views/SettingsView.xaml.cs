using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView()
    {
        InitializeComponent();
        BindingContext = new SettingsViewModel();
    }

    private async void Return_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

}
