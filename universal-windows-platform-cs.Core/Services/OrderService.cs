using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Models;
using UWPApp.PostgreSQL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace universal_windows_platform_cs.Core.Services
{
    public class OrderInfo : Order
    {
        public string CompanyName { get; set; }
    }

    public static class OrderService
    {
        public static async Task<bool> Update(Order NewOrder)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Order OldOrder = (Order)db.Order.Where(order => order.OrderId == NewOrder.OrderId).First();
                    if (OldOrder != null)
                    {
                        OldOrder.OrderTotal = NewOrder.OrderTotal;
                        OldOrder.Status = NewOrder.Status;
                        db.Order.Update(OldOrder);
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

        public static async Task<bool> UpdateByOrderId(long OrderId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    Order OldOrder = (Order)db.Order.Where(order => order.OrderId == OrderId).First();
                    long OrderTotal = 0;
                    if (OldOrder != null)
                    {
                        List<Product> _Product = db.Product.Where(product => product.OrderId == OrderId).ToList();
                        foreach (Product item in _Product)
                        {
                            OrderTotal += (long)item.Total;
                        }
                        OldOrder.OrderTotal = OrderTotal;
                        db.Order.Update(OldOrder);
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

        public static async Task<bool> Add(Order Order)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    db.Order.Add(Order);
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

        public static async Task<List<OrderInfo>> GetAll()
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    List<OrderInfo> Order = db.Order
                        .Where(order => true)
                        .OrderBy(order => order.OrderId)
                        .Join(db.Company, order => order.CompanyId, company => company.CompanyId, 
                            (order, company) => new { Order = order, Company = company })
                        .Select(info => new OrderInfo() {
                            OrderId = info.Order.OrderId,
                            CompanyId = info.Order.CompanyId,
                            OrderDate = info.Order.OrderDate,
                            RequiredDate = info.Order.RequiredDate,
                            ShippedDate = info.Order.ShippedDate,
                            ShipperName = info.Order.ShipperName,
                            ShipperPhone = info.Order.ShipperPhone,
                            OrderTotal = info.Order.OrderTotal,
                            Details = info.Order.Details,
                            Freight = info.Order.Freight,
                            ShipTo = info.Order.ShipTo,
                            Status = info.Order.Status,
                            CompanyName = info.Company.CompanyName
                        })
                        .ToList();
                    return Order;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            }
        }

        public static async Task<OrderInfo> GetByOrderId(long OrderId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    OrderInfo Order = db.Order
                        .Where(order => true)
                        .OrderBy(order => order.OrderId)
                        .Join(db.Company, order => order.CompanyId, company => company.CompanyId,
                            (order, company) => new { Order = order, Company = company })
                        .Select(info => new OrderInfo()
                        {
                            OrderId = info.Order.OrderId,
                            CompanyId = info.Order.CompanyId,
                            OrderDate = info.Order.OrderDate,
                            RequiredDate = info.Order.RequiredDate,
                            ShippedDate = info.Order.ShippedDate,
                            ShipperName = info.Order.ShipperName,
                            ShipperPhone = info.Order.ShipperPhone,
                            OrderTotal = info.Order.OrderTotal,
                            Details = info.Order.Details,
                            Freight = info.Order.Freight,
                            ShipTo = info.Order.ShipTo,
                            Status = info.Order.Status,
                            CompanyName = info.Company.CompanyName
                        }).First();
                    return Order;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return null;
                }
            }
        }

        public static async Task<bool> Remove(long OrderId)
        {
            await Task.CompletedTask;
            using (var db = new UWPContext())
            {
                try
                {
                    List<Product> _Product = db.Product.Where(product => product.OrderId == OrderId).ToList();
                    if (_Product != null)
                    {
                        foreach (var item in _Product)
                        {
                            db.Product.Remove(item);
                        }
                    }
                    Order _Order = db.Order.Where(order => order.OrderId == OrderId).FirstOrDefault();
                    if (_Order != null)
                    {
                        db.Order.Remove(_Order);
                    }
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
