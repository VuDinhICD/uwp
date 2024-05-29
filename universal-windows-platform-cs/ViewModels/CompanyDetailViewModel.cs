using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.Views;

namespace universal_windows_platform_cs.ViewModels
{
    public class CompanyDetailView : Company
    {
        public string FullAddress { get; set; }
    }

    public class CompanyDetailViewModel : ObservableObject
    {
        public CompanyDetailView Item { get; set; }
        public ICommand RemoveCompnayCommand { get; set; }

        public CompanyDetailViewModel()
        {
            RemoveCompnayCommand = new RelayCommand<int>(RemoveCompany);
        }

        public async Task InitializeAsync(int CompanyId)
        {
            var data = await CompanyService.GetByCompanyId(CompanyId);
            Item = new CompanyDetailView()
            {
                CompanyId = data.CompanyId,
                CompanyName = data.CompanyName,
                Address = data.Address,
                City = data.City,
                Country = data.Country,
                ContactName = data.ContactName,
                PostalCode = data.PostalCode,
                Fax = data.Fax,
                ContactTitle = data.ContactTitle,
                Orders = data.Orders,
                Phone = data.Phone,
                FullAddress = $"{data.ContactName} {data.Address}, {data.City}, {data.Country}"
            };
        }

        private async void RemoveCompany(int CompnayId)
        {
            await CompanyService.Remove(CompnayId);
            NavigationService.Navigate<CompanyListPage>();
        }
    }
}
