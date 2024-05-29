using Microsoft.EntityFrameworkCore.Migrations;
using System;
using universal_windows_platform_cs.Core.Models;

namespace universalwindowsplatformcs.Migration.Migrations
{
    public partial class AddProduct : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        private readonly string Name = "Product";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"Migrations Up Table: {Name}");
            migrationBuilder.InsertData(
            table: "Product",
            columns: new[] {
                nameof(Product.OrderId),
                nameof(Product.ProductName),
                nameof(Product.Quantity),
                nameof(Product.Discount),
                nameof(Product.QuantityPerUnit),
                nameof(Product.UnitPrice),
                nameof(Product.CategoryName),
                nameof(Product.CategoryDescription),
                nameof(Product.Total)
            },
            values: new object[,] {
                { 1, "Rössle Sauerkraut", 15, 0.25, "25 - 825 g cans", 45.60, "Produce", "Dried fruit and bean curd", 513.00  },
                { 1, "Chartreuse verte", 21, 0.25, "750 cc per bottle", 45.60, "Beverages", "Soft drinks, coffees, teas, beers, and ales", 513.00  },
                { 1, "Spegesild", 21, 0.25, "4 - 450 g glasses", 45.60, "Seafood", "Seaweed and fish", 18.0  },
                { 2, "Rössle Sauerkraut", 15, 0.25, "25 - 825 g cans", 45.60, "Produce", "Dried fruit and bean curd", 513.00  }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"Migrations Down Table: {Name}");
            migrationBuilder.DeleteData(
            table: "Product",
            keyColumns: new[] {
                nameof(Product.ProductName)
            },
            keyValues: new object[] {
                "Rössle Sauerkraut"
            });

            migrationBuilder.DeleteData(
            table: "Product",
            keyColumns: new[] {
                nameof(Product.ProductName)
            },
            keyValues: new object[] {
                "Chartreuse verte"
            });

            migrationBuilder.DeleteData(
            table: "Product",
            keyColumns: new[] {
                nameof(Product.ProductName)
            },
            keyValues: new object[] {
                "Spegesild"
            });
        }
    }
}
