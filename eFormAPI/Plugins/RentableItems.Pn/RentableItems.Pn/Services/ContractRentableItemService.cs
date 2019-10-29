using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Services
{
    public class ContractRentableItemService : IContractRentableItemService
    {
        private readonly ILogger<ContractRentableItemService> _logger;
        private readonly IRentableItemsLocalizationService _rentableItemsLocalizationService;
        private readonly eFormRentableItemPnDbContext _dbContext;
        private readonly IEFormCoreService _coreHelper;
        private readonly CustomersPnDbAnySql _customerDbContext;
        
        public ContractRentableItemService(eFormRentableItemPnDbContext dbContext,
            ILogger<ContractRentableItemService> logger,
            IEFormCoreService coreHelper,
            IRentableItemsLocalizationService rentableItemLocalizationService,
            CustomersPnDbAnySql customerDbContext
        )
        {
            _dbContext = dbContext;
            _logger = logger;
            _coreHelper = coreHelper;
            _rentableItemsLocalizationService = rentableItemLocalizationService;
            _customerDbContext = customerDbContext;

        }
        public async Task<OperationDataResult<RentableItemsModel>> GetAllRentableItemsOnContract(int contractId)
        {
            try
            {
                RentableItemsModel rentableItemsModel = new RentableItemsModel();
                IQueryable<ContractRentableItem> itemContractsQuery =
                    _dbContext.ContractRentableItem.Where(x => x.ContractId == contractId);
                foreach (var rentableItemContract in itemContractsQuery)
                {
                    RentableItemModel rentableItemModel = new RentableItemModel();
                    RentableItem rentableItem = await
                        _dbContext.RentableItem.FirstOrDefaultAsync(y => y.Id == rentableItemContract.RentableItemId);
                    rentableItemModel.Brand = rentableItem.Brand;
                    rentableItemModel.VinNumber = rentableItem.VinNumber;
                    rentableItemModel.RegistrationDate = rentableItem.RegistrationDate;
                    rentableItemModel.PlateNumber = rentableItem.PlateNumber;
                    rentableItemModel.ModelName = rentableItem.ModelName;
                    rentableItemModel.SerialNumber = rentableItem.SerialNumber;
                    rentableItemModel.Id = rentableItem.Id;
                    rentableItemsModel.RentableItems.Add(rentableItemModel);
                }
                return new OperationDataResult<RentableItemsModel>(true, rentableItemsModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new OperationDataResult<RentableItemsModel>(false, $"Could not find any rentable items");
            }
        }
    }
}