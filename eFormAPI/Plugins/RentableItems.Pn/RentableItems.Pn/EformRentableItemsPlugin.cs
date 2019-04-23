using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microting.eFormApi.BasePn;
using Microting.eFormApi.BasePn.Infrastructure.Database.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.Application;
using Microting.eFormApi.BasePn.Infrastructure.Settings;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Factories;
using RentableItems.Pn.Infrastructure.Data.Seed;
using RentableItems.Pn.Infrastructure.Data.Seed.Data;
using RentableItems.Pn.Infrastructure.Models;
using RentableItems.Pn.Services;


namespace RentableItems.Pn
{
    public class EformRentableItemsPlugin : IEformPlugin
    {
        private string _pluginPath;
        public string Name => "Microting Rentable Items plugin";
        public string PluginId => "eform-angular-rentableitem-plugin";
        public string PluginPath => PluginAssembly().Location;
        private string _connectionString;

        public Assembly PluginAssembly()
        {
            return typeof(EformRentableItemsPlugin).GetTypeInfo().Assembly;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRentableItemsService, RentableItemsService>();
            services.AddScoped<IRentableItemsSettingsService, RentableItemsSettingsService>();
            services.AddScoped<IContractsService, ContractService>();
            services.AddScoped<IContractsInspectionService, ContractsInspectionService>();
            services.AddSingleton<IRentableItemsLocalizationService, RentableItemLocalizationService>();
            services.AddSingleton<IRebusService, RebusService>();

        }
        public void AddPluginConfig(IConfigurationBuilder builder, string connectionString)
        {
            var seedData = new RentableItemsConfiguraitonSeedData();
            var contextFactory = new RentableItemsPnContextFactory();
            builder.AddPluginConfiguration(
                connectionString,
                seedData,
                contextFactory);
        }
        public void ConfigureOptionsServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigurePluginDbOptions<RentableItemBaseSettings>(
                configuration.GetSection("RentableItemBaseSettings"));
        }
        public void ConfigureDbContext(IServiceCollection services, string connectionString)
        {
            _connectionString = connectionString;
            if (connectionString.ToLower().Contains("convert zero datetime"))
            {
                services.AddDbContext<RentableItemsPnDbContext>(o => o.UseMySql(connectionString,
                    b => b.MigrationsAssembly(PluginAssembly().FullName)));
            }
            else
            {
                services.AddDbContext<RentableItemsPnDbContext>(o => o.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(PluginAssembly().FullName)));
            }

            RentableItemsPnContextFactory contextFactory = new RentableItemsPnContextFactory();

            using (var context = contextFactory.CreateDbContext(new[] {connectionString}))
            {  
                context.Database.Migrate();
            }

            // Seed database
            SeedDatabase(connectionString);
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
            var serviceProvider = appBuilder.ApplicationServices;
            IRebusService rebusService = serviceProvider.GetService<IRebusService>();
            rebusService.Start(_connectionString);
        }

        public MenuModel HeaderMenu(IServiceProvider serviceProvider)
        {
            var localizationService = serviceProvider
                .GetService<IRentableItemsLocalizationService>();

            MenuItemModel rentableItem = new MenuItemModel()
            {
                Name = localizationService.GetString("Rentable Items"),
                E2EId = "",
                Link = "/plugins/rentable-items-pn"

            };
            MenuItemModel contracts = new MenuItemModel()
            {
                Name = localizationService.GetString("Contracts"),
                E2EId = "",
                Link = "/plugins/rentable-items-pn/contracts"

            };
            MenuItemModel inspeciton = new MenuItemModel()
            {
                Name = localizationService.GetString("Inspections"),
                E2EId = "",
                Link = "/plugins/rentable-items-pn/inspections"

            };
            MenuItemModel settings = new MenuItemModel()
            {
                Name = localizationService.GetString("Settings"),
                E2EId = "",
                Link = "/plugins/rentable-items-pn/settings"

            };
            List<MenuItemModel> items = new List<MenuItemModel>();
            items.Add(rentableItem);
            items.Add(contracts);
            items.Add(inspeciton);
            items.Add(settings);
            MenuModel result = new MenuModel();
            result.LeftMenu.Add(new MenuItemModel()
            {
                Name = localizationService.GetString("Rentable Items"),
                E2EId = "",
                Link = "/plugins/rentable-items-pn",
                MenuItems = items
                
            });
            return result;
        }

        public void SeedDatabase(string connectionString)
        {
            var contextFactory = new RentableItemsPnContextFactory();
            using (var context = contextFactory.CreateDbContext(new []{connectionString}))
                {
                    RentableItemPluginSeed.SeedData(context);
                }
        }
       
    }
}
