/*
The MIT License (MIT)

Copyright (c) 2007 - 2019 microting

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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
            services.AddTransient<IRentableItemsService, RentableItemsService>();
            services.AddTransient<IRentableItemsSettingsService, RentableItemsSettingsService>();
            services.AddTransient<IContractsService, ContractService>();
            services.AddTransient<IContractsInspectionService, ContractsInspectionService>();
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
            var context = contextFactory.CreateDbContext(new[] {connectionString});
            context.Database.Migrate();

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
                Link = "/plugins/rentable-items-pn/rentable-items"

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
        public void ConfigureOptionsServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigurePluginDbOptions<RentableItemBaseSettings>(
                configuration.GetSection("RentableItemBaseSettings"));
        }

       
    }
}
