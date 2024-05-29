using System;
using System.Collections.Generic;

namespace universal_windows_platform_cs.Core.Models
{
    public class Order
    {
        public long OrderId { get; set; }

        public int CompanyId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public string ShipperName { get; set; }

        public string ShipperPhone { get; set; }

        public double Freight { get; set; }

        public string ShipTo { get; set; }

        public double OrderTotal { get; set; }

        public string Status { get; set; }

        public ICollection<Product> Details { get; set; }

        public override string ToString()
        {
            return $"{CompanyId} {Status}";
        }

        public string ShortDescription => $"Order ID: {OrderId}";
    }
}
