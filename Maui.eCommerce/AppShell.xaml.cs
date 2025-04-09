using Maui.eCommerce.Views;

namespace Maui.eCommerce
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ProductDetailView", typeof(ProductDetailView));
            Routing.RegisterRoute("CheckoutDetailView", typeof(CheckoutDetailView));
        }
    }
}
