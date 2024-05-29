using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migration.PostgreSQL;
using System.IO;

namespace Migration
{
    internal class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            services.AddDbContext<UWPContext>(options =>
            {
                options.UseNpgsql(UWPContext.GetConfigDB(config));
            });
        }
    }
}
