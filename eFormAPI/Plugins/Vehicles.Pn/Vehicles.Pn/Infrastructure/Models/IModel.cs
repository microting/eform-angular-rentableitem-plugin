using System;
using System.Collections.Generic;
using System.Text;
using RentableItems.Pn.Infrastructure.Data;

namespace RentableItems.Pn.Infrastructure.Models
{
    public interface IModel
    {
        void Save(RentableItemsPnDbMSSQL _dbContext);

        void Update(RentableItemsPnDbMSSQL _dbContext);

        void Delete(RentableItemsPnDbMSSQL _dbContext);

    }
}
