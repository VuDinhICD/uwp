using Microsoft.EntityFrameworkCore;
using System.Configuration;
using universal_windows_platform_cs.Core.Models;

namespace UWPApp.PostgreSQL
{
    public class UWPContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString;
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            connString = connectionStrings["PostgreSQLConnectionString"].ToString();
            optionsBuilder.UseNpgsql(connString);
        }
    }
}