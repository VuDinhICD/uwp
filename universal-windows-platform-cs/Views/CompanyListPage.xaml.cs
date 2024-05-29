using PassportLogin.AuthService;
using System.Threading.Tasks;
using universal_windows_platform_cs.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class CompanyListPage : Page
    {
        public CompanyListViewModel ViewModel { get; } = new CompanyListViewModel();
        private AuthService authService = new AuthService();

        public CompanyListPage()
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
        }
    }
}
