using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using universal_windows_platform_cs.Core.Models;

namespace universalwindowsplatformcs.Migration.Migrations
{
    public partial class AddOrder : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        private readonly string Name = "Order";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"Migrations Up Table: {Name}");
            migrationBuilder.InsertData(
            table: "Order",
            columns: new[] {
                nameof(Order.CompanyId),
                nameof(Order.OrderDate),
                nameof(Order.RequiredDate),
                nameof(Order.ShippedDate),
                nameof(Order.ShipperName),
                nameof(Order.ShipperPhone),
                nameof(Order.Freight),
                nameof(Order.ShipTo),
                nameof(Order.OrderTotal),
                nameof(Order.Status)
            },
            values: new object[,] {
                { 
                    1, new DateTime(2023, 12, 12), new DateTime(2023, 12, 12), new DateTime(2023, 12, 12), 
                    "Speedy Express", "(503) 555-2112", 29.46, "Company A Obere Str. 57, Berlin, 12209, Germany",
                    814.50, "Shipped"
                },
                {
                    2, new DateTime(2023, 12, 12), new DateTime(2023, 12, 12), new DateTime(2023, 12, 12),
                    "Maria Anders", "(503) 555-9831", 11.12, "Company B Avda. de la Constitución",
                    321.22, "Shipped"
                }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine($"Migrations Down Table: {Name}");
            migrationBuilder.Sql("DELETE FROM \"Order\" WHERE \"Order\".\"OrderDate\" = timestamp '2023-12-12 00:00:00'");
        }
    }
}
