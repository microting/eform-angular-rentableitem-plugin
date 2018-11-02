using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Models;
using System;

namespace RentableItems.Pn.Tests
{
    [TestFixture]
    public class RentableItemUTest :DbTestFixture
    {
        RentableItemsPnDbAnySql rentableItemsPnDbAnySql;

        public override void DoSetup()
        {

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();

            if (ConnectionString.ToLower().Contains("convert zero datetime"))
            {
                dbContextOptionsBuilder.UseMySql(ConnectionString);
            }
            else
            {
                dbContextOptionsBuilder.UseSqlServer(ConnectionString);
            }
            //dbContextOptionsBuilder.UseLazyLoadingProxies(true);
            rentableItemsPnDbAnySql = new RentableItemsPnDbAnySql(dbContextOptionsBuilder.Options);
        }

        [Test]
        public void RentableItemModel_Save_DoesSave()
        {

            // Arrange
            RentableItemModel rentableItemModel = new RentableItemModel();
            rentableItemModel.Brand = Guid.NewGuid().ToString();
            rentableItemModel.ModelName = Guid.NewGuid().ToString();
            rentableItemModel.PlateNumber = Guid.NewGuid().ToString();
            rentableItemModel.VinNumber = Guid.NewGuid().ToString();
            rentableItemModel.WorkflowState = eFormShared.Constants.WorkflowStates.Created;
            rentableItemModel.SerialNumber = Guid.NewGuid().ToString();
            DateTime registrationDate = DateTime.Now;
            rentableItemModel.RegistrationDate = registrationDate;

            // Act
            rentableItemModel.Save(rentableItemsPnDbAnySql);

            // Assert

            //rentableItemsPnDbAnySql.RentableItem.
        }
    }
}
