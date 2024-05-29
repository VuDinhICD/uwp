using Microsoft.UI.Xaml.Controls;
using PassportLogin.AuthService;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class CompanyAddPage : Page
    {
        public CompanyAddViewModel ViewModel { get; } = new CompanyAddViewModel();
        public static Company CompanyItem { get; set; }
        private AuthService authService = new AuthService();
        public CompanyAddPage()
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            InitializeComponent();
            CompanyItem = new Company();
            LoadingControl.IsLoading = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            base.OnNavigatedTo(e);
            LoadingControl.IsLoading = false;
        }

        private void CompanyNameText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxCompanyName = (TextBox)sender;
            CompanyItem.CompanyName = TextBoxCompanyName.Text;
        }

        private void ContactNameText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxContactName = (TextBox)sender;
            CompanyItem.ContactName = TextBoxContactName.Text;
        }

        private void ContactTitleText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxContactTitle = (TextBox)sender;
            CompanyItem.ContactTitle = TextBoxContactTitle.Text;
        }

        private void PhoneChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            CompanyItem.Phone = args.NewValue.ToString();
        }

        private void FaxChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            CompanyItem.Fax = args.NewValue.ToString();
        }

        private void AddressText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxAddress = (TextBox)sender;
            CompanyItem.Address = TextBoxAddress.Text;
        }

        private void CityText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxCity = (TextBox)sender;
            CompanyItem.City = TextBoxCity.Text;
        }

        private void CountryText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxCountry = (TextBox)sender;
            CompanyItem.Country = TextBoxCountry.Text;
        }

        private void PostalCodeChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            CompanyItem.PostalCode = args.NewValue.ToString();
        }
    }
}
