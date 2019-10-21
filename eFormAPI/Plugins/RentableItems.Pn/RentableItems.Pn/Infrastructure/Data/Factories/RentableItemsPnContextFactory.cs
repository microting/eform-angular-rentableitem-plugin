using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RentableItems.Pn.Infrastructure.Data.Factories
{
    public class RentableItemsPnContextFactory : IDesignTimeDbContextFactory<RentableItemsPnDbContext>
    {
        public RentableItemsPnDbContext CreateDbContext(string[] args)
        {
            //args = new[]
            //    {"data source=.\\sqlexpress;database=appointments-plugin;integrated security=true"};
            var optionsBuilder = new DbContextOptionsBuilder<RentableItemsPnDbContext>();
            if (args.Any())
            {
                if (args.FirstOrDefault().ToLower().Contains("convert zero datetime"))
                {
                    optionsBuilder.UseMySql(args.FirstOrDefault());
                }
                else
                {
                    optionsBuilder.UseSqlServer(args.FirstOrDefault());
                }
            }
            else
            {
                throw new ArgumentNullException("Connection string not present");
            }
//            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=555_RentableItems;Integrated Security=True;");
            //            dotnet ef migrations add InitialCreate --project RentableItems.Pn --startup-project DBMigrator
            optionsBuilder.UseLazyLoadingProxies(true);
            return new RentableItemsPnDbContext(optionsBuilder.Options);
        }
    }
}
