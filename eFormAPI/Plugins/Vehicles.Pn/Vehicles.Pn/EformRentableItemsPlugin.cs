using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microting.eFormApi.BasePn;
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
