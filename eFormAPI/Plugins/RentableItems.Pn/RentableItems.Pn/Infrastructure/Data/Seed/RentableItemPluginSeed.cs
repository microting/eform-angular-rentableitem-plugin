using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using System;
using System.Linq;
using Microting.eForm.Infrastructure.Constants;
using RentableItems.Pn.Infrastructure.Data.Seed.Data;

namespace RentableItems.Pn.Infrastructure.Data.Seed
{
    public class RentableItemPluginSeed
    {
        public static void SeedData(RentableItemsPnDbContext dbContext)
        {
            var seedData = new RentableItemsConfiguraitonSeedData();
            var configurationList = seedData.Data;
            foreach (var configurationItem in configurationList)
            {
                if (!dbContext.PluginConfigurationValues.Any(x=>x.Name == configurationItem.Name))
                {
                    var newConfigValue = new PluginConfigurationValue()
                    {
                        Name = configurationItem.Name,
                        Value = configurationItem.Value,
                        CreatedAt = DateTime.UtcNow,
                        Version = 1,
                        WorkflowState = Constants.WorkflowStates.Created,
                        CreatedByUserId = 1
                    };
                    dbContext.PluginConfigurationValues.Add(newConfigValue);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}