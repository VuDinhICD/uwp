using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Models;
using UWPApp.PostgreSQL;

namespace universal_windows_platform_cs.Core.Services
{
    public static class ProductService
    {
        public static async Task<List<Product>> GetByOrderId(long OrderId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    List<Product> data = db.Product.Where(product => product.OrderId == OrderId).ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            }
        }

        public static async Task<bool> Add(Product Product)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    db.Product.Add(Product);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
            }
        }

        public static async Task<bool> Update(Product NewProduct)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Product OldProduct = (Product)db.Product.Where(product => product.ProductId == NewProduct.ProductId).First();
                    if (OldProduct != null)
                    {
                        OldProduct.ProductName = NewProduct.ProductName;
                        OldProduct.Quantity = NewProduct.Quantity;
                        OldProduct.UnitPrice = NewProduct.UnitPrice;
                        OldProduct.CategoryName = NewProduct.CategoryName;
                        OldProduct.CategoryDescription = NewProduct.CategoryDescription;
                        OldProduct.Total = NewProduct.Quantity * NewProduct.UnitPrice;
                        db.Product.Update(OldProduct);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
            }
        }

        public static async Task<bool> Remove(long ProductId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Product _Product = db.Product.Where(product => product.ProductId == ProductId).FirstOrDefault();
                    if (_Product != null)
                    {
                        db.Product.Remove(_Product);
                        db.SaveChanges();
                        return true;
                    } 
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
            }
        }
    }
}
