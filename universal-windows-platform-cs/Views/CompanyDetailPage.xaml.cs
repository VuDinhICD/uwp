using Microsoft.Toolkit.Uwp.UI.Animations;
using PassportLogin.AuthService;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class CompanyDetailPage : Page
    {
        public CompanyDetailViewModel ViewModel { get; } = new CompanyDetailViewModel();
        private AuthService authService = new AuthService();
        public CompanyDetailPage()
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            base.OnNavigatedTo(e);
            this.RegisterElementForConnectedAnimation("animationKeyCompanyDetail", itemHero);
            if (e.Parameter is int CompanyId)
            {
                await ViewModel.InitializeAsync(CompanyId);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
