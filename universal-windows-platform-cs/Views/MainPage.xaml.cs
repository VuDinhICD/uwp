using PassportLogin.AuthService;
// using Logger;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        public AccountUser AccountInfo { get; set; } = new AccountUser();
        private AuthService authService = new AuthService();

        public MainPage()
        {
            if (authService.IsLoginedIn())
            {
                return;
            }
            InitializeComponent();
            LoadingControl.IsLoading = true;
            DataContext = ViewModel;
            // Debug.WriteLine(Logging.LogPath);
            // Logging.Info("Insert logging to file!");
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (authService.IsLoginedIn())
            {
                return;
            }
            base.OnNavigatedTo(e);
            await Task.Delay(100);
            await ViewModel.LoadDataAsync();
            LoadingControl.IsLoading = false;
        }

        private void SubmitAccount(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AccountInfo.username = User.Text;
            AccountInfo.password = Password.Password.ToString();
        }

        private void ForgotAccount(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AccountInfo.username = User.Text;
            AccountInfo.password = Password.Password.ToString();
            string mess;
            if (AuthCoreService.GetUserByUserName(User.Text))
            {
                mess = "Mật khẩu đã được gửi qua email";
            }
            else
            {
                mess = "Kiểm tra lại tài khoản";
            }

            MessageDialog messageDialog = new MessageDialog(mess);
            messageDialog.ShowAsync();
        }
    }
}
