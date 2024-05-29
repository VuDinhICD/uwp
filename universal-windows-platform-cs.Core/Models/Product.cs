using Microsoft.Extensions.Logging;

namespace universal_windows_platform_cs.Core.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public long OrderId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Discount { get; set; }

        public string QuantityPerUnit { get; set; }

        public double UnitPrice { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public double Total { get; set; }

        public string ShortDescription => $"Product ID: {ProductId} - {ProductName}";
    }
}
