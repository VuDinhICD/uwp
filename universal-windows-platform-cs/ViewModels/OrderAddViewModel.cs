using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Controls.TextToolbarSymbols;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.Views;

namespace universal_windows_platform_cs.ViewModels
{
    public class OrderCreate
    {
        public int CompanyId { get; set; }
        public string ShipperName { get; set; }
    }

    public class OrderAddViewModel : ObservableObject
    {
        public ICommand CreateOrderCommand { get; set; }
        public ObservableCollection<string> CompanyName { get; set; }
        public string ShipTo { get; set; }

        public OrderAddViewModel()
        {
            CompanyName = new ObservableCollection<string>();
            CreateOrderCommand = new RelayCommand(CreateOrder);
            CompanyName = new ObservableCollection<string>() { "" };
            ShipTo = "";
        }

        public async Task LoadDataAsync()
        {
            CompanyName.Clear();
            List<Company> companies = await CompanyService.GetAll();
            foreach (Company company in companies)
            {
                CompanyName.Add(company.CompanyName);
            }
        }
        private static async void CreateOrder()
        {
            await OrderService.Add(OrderAddPage.OrderItem);
            NavigationService.Navigate<OrderListPage>();
        }

        public async Task<Company> InfoCompany(string CompanyName)
        {
            Company _Company =  await CompanyService.GetByCompanyName(CompanyName);
            if (_Company != null)
            {
                return _Company;
            }
            return null;
        }
    }
}
