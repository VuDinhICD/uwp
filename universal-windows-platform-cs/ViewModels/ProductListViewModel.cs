using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public class ProductList : Product
    {
        public ICommand RemoveProductCommand { get; set; }

        public ProductList()
        {
            RemoveProductCommand = new RelayCommand<long>(RemoveProduct);
        }

        private static async void RemoveProduct(long ProductId)
        {
            await ProductService.Remove(ProductId);
            await OrderService.UpdateByOrderId(ProductListViewModel.OrderId);
        }
    }

    public class ProductListViewModel : ObservableObject
    {
        public ICommand AddProductCommand { get; set; }
        public ObservableCollection<ProductList> Source { get; } = new ObservableCollection<ProductList>();
        public static long OrderId { get; set; }

        public ProductListViewModel()
        {
            AddProductCommand = new RelayCommand(PageNewProduct);
        }

        public async Task InitializeAsync(long OrderId)
        {
            ProductListViewModel.OrderId = OrderId;
            Source.Clear();
            await Task.CompletedTask;
            List<Product> data = await ProductService.GetByOrderId(OrderId);
            foreach (var item in data)
            {
                ProductList productList = new ProductList() {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    CategoryName = item.CategoryName,
                    CategoryDescription = item.CategoryDescription,
                    OrderId = item.OrderId,
                    Quantity = item.Quantity,
                    QuantityPerUnit = item.QuantityPerUnit,
                    Total = item.Total,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                };
                Source.Add(productList);
            }
        }
        private static void PageNewProduct()
        {
            NavigationService.Navigate<ProductAddPage>(ProductListViewModel.OrderId);
        }
    }
}
