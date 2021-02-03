using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using NUnit.Framework;

namespace RentableItems.Pn.Tests
{
    [TestFixture]
    public abstract class DbTestFixture
    {

        protected eFormRentableItemPnDbContext DbContext;
        protected string ConnectionString;

        //public RentableItemsPnDbAnySql db;

        public void GetContext(string connectionStr)
        {

            DbContextOptionsBuilder<eFormRentableItemPnDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<eFormRentableItemPnDbContext>();

            dbContextOptionsBuilder.UseMySql(connectionStr);
            DbContext = new eFormRentableItemPnDbContext(dbContextOptionsBuilder.Options);

            DbContext.Database.Migrate();
            DbContext.Database.EnsureCreated();
        }

        [SetUp]
        public void Setup()
        {
                ConnectionString = @"Server = localhost; port = 3306; Database = rentable-items-pn-tests; user = root; Convert Zero Datetime = true;";

            GetContext(ConnectionString);
            DbContext.Database.SetCommandTimeout(300);

            try
            {
                ClearDb();
            }
            catch
            {
                DbContext.Database.Migrate();
            }

            DoSetup();
        }

        [TearDown]
        public void TearDown()
        {
            ClearDb();

            ClearFile();

            DbContext.Dispose();
        }

        public void ClearDb()
        {
            List<string> modelNames = new List<string>
            {
                "RentableItemsContractVersions",
                "RentableItemContract",
                "ContractInspectionVersion",
                "ContractInspection",
                "ContractVersions",
                "Contract",
                "RentableItemsSettingsVersions",
                "RentableItemsSettings",
                "RentableItemsVersion",
                "RentableItem"
            };

            bool firstRunNotDone = true;

            foreach (var modelName in modelNames)
            {
                try
                {
                    if (firstRunNotDone)
                    {
                        DbContext.Database.ExecuteSqlRaw(
                            $"SET FOREIGN_KEY_CHECKS = 0;TRUNCATE `rentable-items-pn-tests`.`{modelName}`");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Unknown database 'rentable-items-pn-tests'")
                    {
                        firstRunNotDone = false;
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        private string _path;

        public void ClearFile()
        {
            _path = Assembly.GetExecutingAssembly().CodeBase;
            _path = Path.GetDirectoryName(_path).Replace(@"file:\", "");

            string picturePath = _path + @"\output\dataFolder\picture\Deleted";

            DirectoryInfo diPic = new DirectoryInfo(picturePath);

            try
            {
                foreach (FileInfo file in diPic.GetFiles())
                {
                    file.Delete();
                }
            }
            catch
            {
                // ignored
            }
        }
        public virtual void DoSetup() { }

    }
}
