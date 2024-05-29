using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using universal_windows_platform_cs.Core.Models;
using static System.Collections.Specialized.BitVector32;

namespace Migration.PostgreSQL
{
    public class UWPContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            optionsBuilder.UseNpgsql(GetConfigDB(config));
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(company => { });
            modelBuilder.Entity<Order>(order => { });
            modelBuilder.Entity<Product>(product => { });
            base.OnModelCreating(modelBuilder);
        }

        public static string GetConfigDB(IConfiguration config)
        {
            Console.WriteLine("GetConfigDB ...");
            string connString;
            try
            {
                connString = config["ConnectionString:PostgreSQL"];
                Console.WriteLine($"ConnectionString: {connString}");
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                connString = "Host=localhost;Username=postgres;Password=root;Database=UWP_CS";
                Console.WriteLine($"Set default ConnectionString");
            }
            
            return connString;
        }
    }
}