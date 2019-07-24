using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;

namespace RentableItems.Pn.Infrastructure.Data.Seed.Data
{
    public class RentableItemsConfiguraitonSeedData : IPluginConfigurationSeedData
    {
        public PluginConfigurationValue[] Data => new[]
        {
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:LogLevel",
                Value = "4"
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:LogLimit",
                Value = "25000"
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:SdkConnectionString",
                Value = "..."
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:MaxParallelism",
                Value = "1"
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:NumberOfWorkers",
                Value = "1"
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:SdkeFormId",
                Value = "..."
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:EnabledSiteIds",
                Value = ""
            }

            
        };
    }
}