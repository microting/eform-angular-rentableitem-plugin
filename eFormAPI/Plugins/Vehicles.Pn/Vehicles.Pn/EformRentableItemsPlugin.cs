using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microting.eFormApi.BasePn;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
//using RentableItems.Pn.Infrastructure.Data.Factories;
using RentableItems.Pn.Services;

namespace RentableItems.Pn
{
    public class EformRentableItemsPlugin : IEformPlugin
    {
        public string GetName() => "Microting Rentable plugin";
        public string ConnectionStringName() => "EFormVehiclesPnConnection";
        public string PluginPath() => PluginAssembly().Location;

        public Assembly PluginAssembly()
        {
            return typeof(EformRentableItemsPlugin).GetTypeInfo().Assembly;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRentableItemsService, RentableItemsService>();
            services.AddSingleton<IRentableItemsLocalizationService, RentableItemLocalizationService>();
        }

        public void ConfigureDbContext(IServiceCollection services, string connectionString)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();

            if (connectionString.ToLower().Contains("convert zero datetime"))
            {
                dbContextOptionsBuilder.UseMySQL(connectionString);
                services.AddDbContext<RentableItemsPnDbAnySql>(o => o.UseMySQL(connectionString,
                b => b.MigrationsAssembly(PluginAssembly().FullName)));

                dbContextOptionsBuilder.UseLazyLoadingProxies(true);
                var context = new RentableItemsPnDbAnySql(dbContextOptionsBuilder.Options);
                context.Database.Migrate();
            }
            else
            {
                dbContextOptionsBuilder.UseSqlServer(connectionString);
                services.AddDbContext<RentableItemsPnDbAnySql>(o => o.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(PluginAssembly().FullName)));

                dbContextOptionsBuilder.UseLazyLoadingProxies(true);
                var context = new RentableItemsPnDbAnySql(dbContextOptionsBuilder.Options);
                context.Database.Migrate();
            }
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
        }
        
        public void SeedDatabase(string connectionString)
        {
        }
    }
}
