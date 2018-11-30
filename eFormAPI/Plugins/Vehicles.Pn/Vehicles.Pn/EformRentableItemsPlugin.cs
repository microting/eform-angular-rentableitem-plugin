using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microting.eFormApi.BasePn;
using Microting.eFormApi.BasePn.Infrastructure.Models.Application;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Data.Entities;
using RentableItems.Pn.Infrastructure.Data.Factories;
using RentableItems.Pn.Infrastructure.Enums;
using RentableItems.Pn.Infrastructure.Extensions;
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
            services.AddScoped<IRentableItemsSettingsService, RentableItemsSettingsService>();
            services.AddScoped<IContractsService, ContractService>();
            services.AddScoped<IContractsInspectionService, ContractsInspectionService>();
            services.AddSingleton<IRentableItemsLocalizationService, RentableItemLocalizationService>();
        }

        public void ConfigureDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RentableItemsPnDbAnySql>(o => o.UseSqlServer(connectionString,
             b => b.MigrationsAssembly(PluginAssembly().FullName)));

            RentableItemsPnContextFactory contextFactory = new RentableItemsPnContextFactory();
            using (RentableItemsPnDbAnySql context = contextFactory.CreateDbContext(new[] { connectionString }))
            {
                context.Database.Migrate();
            }
            // Seed database
            SeedDatabase(connectionString);
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
        }

        public MenuModel HeaderMenu()
        {
            MenuItemModel rentableItem = new MenuItemModel()
            {
                Name = "Rentable Items",
                E2EId = "",
                Link = "/plugins/rentable-items-pn"

            };
            MenuItemModel contracts = new MenuItemModel()
            {
                Name = "Contracts",
                E2EId = "",
                Link = "/plugins/rentable-items-pn/contracts"

            };
            MenuItemModel inspeciton = new MenuItemModel()
            {
                Name = "Inspections",
                E2EId = "",
                Link = "/plugins/rentable-items-pn/inspections"

            };
            MenuItemModel settings = new MenuItemModel()
            {
                Name = "Settings",
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
                Name = "Rentable Items",
                E2EId = "",
                Link = "/plugins/rentable-items-pn",
                MenuItems = items
                
            });
            return result;
        }

        public void SeedDatabase(string connectionString)
        {
            RentableItemsPnContextFactory contextFactory = new RentableItemsPnContextFactory();
            using (RentableItemsPnDbAnySql context = contextFactory.CreateDbContext(new[] { connectionString }))
            {

                //Add Data
                List<string> rentableItemsFields = new RentableItem().GetPropList();
                foreach (string name in rentableItemsFields)
                {
                    Field field = new Field()
                    {
                        Name = name
                    };
                    if (!context.Fields.Any(x => x.Name == name))
                    {
                        context.Fields.Add(field);
                    }
                }
                context.SaveChanges();

                List<Field> fields = context.Fields.ToList();
                foreach (Field field in fields)
                {
                    RentableItemsField rentableItemsField = new RentableItemsField
                    {
                        FieldId = field.Id,
                        FieldStatus = FieldStatus.Enabled
                    };
                    if (!context.RentableItemsFields.Any(x => x.FieldId == field.Id))
                    {
                        context.RentableItemsFields.Add(rentableItemsField);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
