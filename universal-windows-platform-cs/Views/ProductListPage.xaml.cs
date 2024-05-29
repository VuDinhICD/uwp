using Microsoft.Toolkit.Uwp.UI.Controls;
using PassportLogin.AuthService;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class ProductListPage : Page
    {
        public ProductListViewModel ViewModel { get; } = new ProductListViewModel();
        public string OrderName { get; set; }
        private AuthService authService = new AuthService();
        public ProductListPage()
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            InitializeComponent();
            LoadingControl.IsLoading = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            base.OnNavigatedTo(e);
            if (e.Parameter is long OrderId)
            {
                await ViewModel.InitializeAsync(OrderId);
                OrderName = $"Show all Product of OrderId {OrderId}";
            }
            LoadingControl.IsLoading = false;
        }

        private async void UpdateProductData(object sender, DataGridCellEditEndedEventArgs e)
        {
            Product UpdateProduct = (Product)e.Row.DataContext;
            if (UpdateProduct != null)
            {
                await ProductService.Update(UpdateProduct);
                await OrderService.UpdateByOrderId(UpdateProduct.OrderId);
                await ViewModel.InitializeAsync(UpdateProduct.OrderId);
            }
        }

        private async void ReloadPage(object sender, RoutedEventArgs e)
        {
            LoadingControl.IsLoading = true;
            await Task.Delay(1000);
            await ViewModel.InitializeAsync(ProductListViewModel.OrderId);
            LoadingControl.IsLoading = false;
        }
    }
}
