using Microsoft.Toolkit.Uwp.UI.Controls;
using PassportLogin.AuthService;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class OrderListPage : Page
    {
        public OrderListViewModel ViewModel { get; } = new OrderListViewModel();
        private AuthService authService = new AuthService();
        public OrderListPage()
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
            await Task.Delay(100);
            await ViewModel.LoadDataAsync();
            LoadingControl.IsLoading = false;
            if (ViewModel.Source.Count == 0)
            {
                NavigationService.Navigate<OrderAddPage>();
            }
        }

        private async void UpdateOrderData(object sender, DataGridCellEditEndedEventArgs e)
        {
            Order UpdateOrder = (Order)e.Row.DataContext;
            if (UpdateOrder != null)
            {
                await OrderService.Update(UpdateOrder);
            }
        }

        private async void ReloadPage(object sender, RoutedEventArgs e)
        {
            LoadingControl.IsLoading = true;
            await Task.Delay(1000);
            await ViewModel.LoadDataAsync();
            LoadingControl.IsLoading = false;
        }
    }
}
