using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Models;
using UWPApp.PostgreSQL;

namespace universal_windows_platform_cs.Core.Services
{
    public static class CompanyService
    {
        public static async Task<List<Company>> GetAll()
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    List<Company> data = db.Company.Where(company => true).ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            }
        }

        public static async Task<Company> GetByCompanyId(int CompanyId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Company data = db.Company.Where(company => company.CompanyId == CompanyId).First();
                    return data;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            }
        }

        public static async Task<Company> GetByCompanyName(string CompanyName)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Company data = db.Company.Where(company => company.CompanyName == CompanyName).First();
                    return data;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            }
        }

        public static async Task<bool> Add(Company Company)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    db.Company.Add(Company);
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

        public static async Task<bool> Remove(int CompanyId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Company _Company = db.Company.Where(company => company.CompanyId == CompanyId).First();
                    List<Order> _Order = db.Order.Where(order => order.CompanyId == _Company.CompanyId).ToList();

                    foreach (Order _OrderItem in _Order)
                    {
                        List<Product> _Product = db.Product.Where(product => product.OrderId == _OrderItem.OrderId).ToList();
                        if (_Product != null)
                        {
                            foreach (Product _ProductItem in _Product)
                            {
                                db.Product.Remove(_ProductItem);
                            }
                        }
                        db.Order.Remove(_OrderItem);
                    }

                    db.Company.Remove(_Company);
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

    }
}
