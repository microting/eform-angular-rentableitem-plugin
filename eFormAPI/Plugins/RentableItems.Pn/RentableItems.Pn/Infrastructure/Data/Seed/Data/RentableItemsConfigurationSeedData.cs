using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;

namespace RentableItems.Pn.Infrastructure.Data.Seed.Data
{
    public class RentableItemsConfigurationSeedData : IPluginConfigurationSeedData
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
                Name = "RentableItemBaseSettings:GmailCredentials",
                Value = ""
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:GmailClientSecret",
                Value = ""
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:GmailEmail",
                Value = ""
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:GmailUserName",
                Value = ""
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:MailFrom",
                Value = ""
            },
            new PluginConfigurationValue()
            {
                Name = "RentableItemBaseSettings:Token",
                Value = "..."
            }
        };
    }
}