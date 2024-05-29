using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace universalwindowsplatformcs.Migration.Migrations
{
    public partial class InitialCreate : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Company",
            columns: table => new
            {
                CompanyId = table.Column<int>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CompanyName = table.Column<string>(nullable: true),
                ContactName = table.Column<string>(nullable: true),
                ContactTitle = table.Column<string>(nullable: true),
                Address = table.Column<string>(nullable: true),
                City = table.Column<string>(nullable: true),
                PostalCode = table.Column<string>(nullable: true),
                Country = table.Column<string>(nullable: true),
                Phone = table.Column<string>(nullable: true),
                Fax = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Company", x => x.CompanyId);
            });

            migrationBuilder.CreateTable(
            name: "Order",
            columns: table => new
            {
                OrderId = table.Column<long>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CompanyId = table.Column<int>(nullable: true),
                OrderDate = table.Column<DateTime>(nullable: false),
                RequiredDate = table.Column<DateTime>(nullable: false),
                ShippedDate = table.Column<DateTime>(nullable: false),
                ShipperName = table.Column<string>(nullable: true),
                ShipperPhone = table.Column<string>(nullable: true),
                Freight = table.Column<double>(nullable: false),
                ShipTo = table.Column<string>(nullable: true),
                OrderTotal = table.Column<double>(nullable: false),
                Status = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Order", x => x.OrderId);
                table.ForeignKey(
                    name: "FK_Order_Company_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "Company",
                    principalColumn: "CompanyId",
                    onDelete: ReferentialAction.Restrict);
            });

            migrationBuilder.CreateTable(
            name: "Product",
            columns: table => new
            {
                ProductId = table.Column<long>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                OrderId = table.Column<long>(nullable: true),
                ProductName = table.Column<string>(nullable: true),
                Quantity = table.Column<int>(nullable: false),
                Discount = table.Column<double>(nullable: false),
                QuantityPerUnit = table.Column<string>(nullable: true),
                UnitPrice = table.Column<double>(nullable: false),
                CategoryName = table.Column<string>(nullable: true),
                CategoryDescription = table.Column<string>(nullable: true),
                Total = table.Column<double>(nullable: false),
            },
            constraints: table =>
            {
            table.PrimaryKey("PK_Product", x => x.ProductId);
            table.ForeignKey(
                name: "FK_Product_Order_OrderId",
                column: x => x.OrderId,
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
            });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CompanyId",
                table: "Order",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
