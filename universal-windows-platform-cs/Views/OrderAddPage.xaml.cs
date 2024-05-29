using Microsoft.UI.Xaml.Controls;
using PassportLogin.AuthService;
using System.Diagnostics;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace universal_windows_platform_cs.Views
{
    public sealed partial class OrderAddPage : Page
    {
        public OrderAddViewModel ViewModel { get; } = new OrderAddViewModel();
        private AuthService authService = new AuthService();
        public static Order OrderItem { get; set; }

        public OrderAddPage()
        {
            if (!(authService.IsLoginedIn()))
            {
                return;
            }
            InitializeComponent();
            OrderItem = new Order();
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
            OrderItem.Status = "Available";
            LoadingControl.IsLoading = false;
        }

        private async void CompanyChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ComboBoxCompany = (ComboBox)sender;
            if (ComboBoxCompany != null)
            {
                string CompanyName = ComboBoxCompany.SelectedValue.ToString();
                Company _Company = await ViewModel.InfoCompany(CompanyName);
                OrderItem.CompanyId = _Company.CompanyId;
                ShipTo.Text = $"{CompanyName} {_Company.Address}, {_Company.City}, {_Company.Country}";
                OrderItem.OrderTotal = 0;
                OrderItem.OrderDate = System.DateTime.Now;
                OrderItem.RequiredDate = System.DateTime.Now;
                OrderItem.ShippedDate = System.DateTime.Now;
            }
        }

        private void ShipperNameText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxShipperName = (TextBox)sender;
            OrderItem.ShipperName = TextBoxShipperName.Text;
        }

        private void ShipToText(object sender, TextChangedEventArgs e)
        {
            TextBox TextBoxShipTo = (TextBox)sender;
            OrderItem.ShipTo = TextBoxShipTo.Text;
        }

        private void ShipperPhoneChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            OrderItem.ShipperPhone = args.NewValue.ToString();
        }

        private void FreightChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            OrderItem.Freight = args.NewValue;
        }

        private void OrderDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            DatePicker DatePickerOrderDate = (DatePicker)sender;
            Debug.WriteLine($"OrderDateChanged {DatePickerOrderDate.SelectedDate.Value}");
            if (DatePickerOrderDate.SelectedDate.HasValue)
            {
                OrderItem.OrderDate = DatePickerOrderDate.SelectedDate.Value.DateTime;
            }
        }

        private void RequiredDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            DatePicker DatePickerRequiredDate = (DatePicker)sender;
            if (DatePickerRequiredDate.SelectedDate.HasValue)
            {
                OrderItem.RequiredDate = DatePickerRequiredDate.SelectedDate.Value.DateTime;
            }
        }

        private void ShippedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            DatePicker DatePickerShippedDate = (DatePicker)sender;
            if (DatePickerShippedDate.SelectedDate.HasValue)
            {
                OrderItem.ShippedDate = DatePickerShippedDate.SelectedDate.Value.DateTime;
            }
        }
    }
}
