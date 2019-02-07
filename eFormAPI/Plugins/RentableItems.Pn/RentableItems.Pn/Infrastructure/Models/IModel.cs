using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentableItems.Pn.Infrastructure.Data;

namespace RentableItems.Pn.Infrastructure.Models
{
    public interface IModel
    {
        Task Save(RentableItemsPnDbAnySql _dbContext);

        Task Update(RentableItemsPnDbAnySql _dbContext);

        Task Delete(RentableItemsPnDbAnySql _dbContext);

    }
}
