using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using universal_windows_platform_cs.Core.Models;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.Views;

namespace universal_windows_platform_cs.ViewModels
{
    public class OrderListView : Order
    {
        public string Company { get; set; }
        public ICommand NavigateCommand { get; set; }
        public ICommand RemoveOrderCommand { get; set; }

        public OrderListView()
        {
            NavigateCommand = new RelayCommand<long>(ViewProductPage);
            RemoveOrderCommand = new RelayCommand<long>(RemoveOrder);
        }

        private static void ViewProductPage(long OrderId)
        {
            if (OrderId > 0)
            {
                NavigationService.Navigate<ProductListPage>(OrderId);
            }
        }

        private static async void RemoveOrder(long OrderId)
        {
            await OrderService.Remove(OrderId);
        }
    }

    public class OrderListViewModel : ObservableObject
    {
        public ICommand CreateOrderCommand { get; set; }
        public ObservableCollection<OrderListView> Source { get; } = new ObservableCollection<OrderListView>();
        public ObservableCollection<string> OrderStatus { get; set; } = new ObservableCollection<string> {
            "Available", "Delivered", "Shipped"
        };

        public OrderListViewModel()
        {
            CreateOrderCommand = new RelayCommand(CreateOrder);
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await OrderService.GetAll();

            foreach (var item in data)
            {
                OrderListView dataItem = new OrderListView
                {
                    OrderId = item.OrderId,
                    CompanyId = item.CompanyId,
                    OrderDate = item.OrderDate,
                    RequiredDate = item.RequiredDate,
                    ShippedDate = item.ShippedDate,
                    ShipperName = item.ShipperName,
                    ShipperPhone = item.ShipperPhone,
                    Freight = item.Freight,
                    ShipTo = item.ShipTo,
                    Status = item.Status,
                    OrderTotal = item.OrderTotal,
                    Details = item.Details,
                    Company = item.CompanyName
                };
                Source.Add(dataItem);
            }
        }

        private static void CreateOrder()
        {
            NavigationService.Navigate<OrderAddPage>();
        }
    }
}
