using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.Views;

namespace universal_windows_platform_cs.ViewModels
{
    public class CompanyAddViewModel : ObservableObject
    {
        public ICommand CreateCompanyCommand { get; set; }
        public CompanyAddViewModel()
        {
            CreateCompanyCommand = new RelayCommand(CreateCompany);
        }

        private static async void CreateCompany()
        {
            await CompanyService.Add(CompanyAddPage.CompanyItem);
            NavigationService.Navigate<CompanyListPage>();
        }
    }
}
