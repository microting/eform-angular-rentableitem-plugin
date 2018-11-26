using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RentableItems.Pn.Infrastructure.Data.Factories
{
    public class RentableItemsPnContextFactory : IDesignTimeDbContextFactory<RentableItemsPnDbAnySql>
    {
        public RentableItemsPnDbAnySql CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<RentableItemsPnDbAnySql>();
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
            optionsBuilder.UseLazyLoadingProxies(true);
            return new RentableItemsPnDbAnySql(optionsBuilder.Options);
        }
    }
}
