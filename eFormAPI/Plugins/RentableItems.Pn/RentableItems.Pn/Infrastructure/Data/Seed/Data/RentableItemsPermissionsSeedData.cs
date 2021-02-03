using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using RentableItems.Pn.Infrastructure.Data.Consts;

namespace RentableItems.Pn.Infrastructure.Data.Seed.Data
{
    public static class RentableItemsPermissionsSeedData
    {
        public static PluginPermission[] Data => new[]
        {
            new PluginPermission
            {
                PermissionName = "Access RentableItems Plugin",
                ClaimName = RentableItemsClaims.AccessRentableItemsPlugin
            },
            new PluginPermission
            {
                PermissionName = "Create Rentable Items",
                ClaimName = RentableItemsClaims.CreateRentableItems
            },
        };
    }
}