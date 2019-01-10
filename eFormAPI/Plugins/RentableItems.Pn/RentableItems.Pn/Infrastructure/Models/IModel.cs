using System;
using System.Collections.Generic;
using System.Text;
using RentableItems.Pn.Infrastructure.Data;

namespace RentableItems.Pn.Infrastructure.Models
{
    public interface IModel
    {
        void Save(RentableItemsPnDbAnySql _dbContext);

        void Update(RentableItemsPnDbAnySql _dbContext);

        void Delete(RentableItemsPnDbAnySql _dbContext);

    }
}
