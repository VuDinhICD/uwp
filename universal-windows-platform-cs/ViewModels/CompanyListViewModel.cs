using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI.Animations;

using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.Views;

namespace universal_windows_platform_cs.ViewModels
{
    public class CompanyListView : Company
    {
        public string Symbol { get; set; }
    }

    public class CompanyListViewModel : ObservableObject
    {
        public ICommand ItemClickCommand { get; set; }

        public ObservableCollection<CompanyListView> Source { get; } = new ObservableCollection<CompanyListView> ();

        public CompanyListViewModel()
        {
            ItemClickCommand = new RelayCommand<CompanyListView>(OnItemClick);
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await CompanyService.GetAll();
            foreach (var item in data)
            {
                CompanyListView dataItem = new CompanyListView
                {
                    CompanyId = item.CompanyId,
                    CompanyName = item.CompanyName,
                    Symbol = "\uE819"
                };
                Source.Add(dataItem);
            }

            Source.Add(new CompanyListView {
                CompanyName = "New",
                Symbol = "\uE710"
            });;;
        }

        private void OnItemClick(CompanyListView clickedItem)
        {
            if (clickedItem != null && clickedItem.CompanyName != "New")
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<CompanyDetailPage>(clickedItem.CompanyId);
            }
            else
            {
                NavigationService.Navigate<CompanyAddPage>();
            }
        }
    }
}
