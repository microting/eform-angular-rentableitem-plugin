using System;
using System.Diagnostics;
using System.Linq;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Data;
using RentableItems.Pn.Infrastructure.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using System.Collections.Generic;
using RentableItems.Pn.Infrastructure.Data.Entities;

namespace RentableItems.Pn.Services
{
    class RentableItemsFieldsService : IRentableItemFieldsService
    {
        private readonly ILogger<RentableItemsFieldsService> _logger;
        private readonly IRentableItemsLocalizationService _localizationService;
        private readonly RentableItemsPnDbContext _dbContext;

        public RentableItemsFieldsService(ILogger<RentableItemsFieldsService> logger,
            RentableItemsPnDbContext dbContext,
            IRentableItemsLocalizationService localizationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _localizationService = localizationService;
        }

        public OperationDataResult<RentableItemsFieldsUpdateModel> GetFields()
        {
            try
            {
                List<RentableItemsFieldUpdateModel> fields = _dbContext.RentableItemsFields
                    .Include("Field")
                    .Select(x => new RentableItemsFieldUpdateModel()
                    {
                        FieldStatus = x.FieldStatus,
                        Id = x.FieldId,
                        Name = x.Field.Name,
                    }).ToList();
                // Mode Id field to top
                int index = fields.FindIndex(x => x.Name == "Id");
                RentableItemsFieldUpdateModel item = fields[index];
                fields[index] = fields[0];
                fields[0] = item;
                RentableItemsFieldsUpdateModel result = new RentableItemsFieldsUpdateModel()
                {
                    Fields = fields,
                };
                return new OperationDataResult<RentableItemsFieldsUpdateModel>(true, result);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<RentableItemsFieldsUpdateModel>(false,
                    _localizationService.GetString("ErrorWhileObtainingFieldsInfo"));
            }
        }

        public OperationResult UpdateFields(RentableItemsFieldsUpdateModel rentableItemsFieldsModel)
        {
            try
            {
                List<int> list = rentableItemsFieldsModel.Fields.Select(s => s.Id).ToList();
                List<RentableItemsField> fields = _dbContext.RentableItemsFields
                    .Where(x => list.Contains(x.FieldId))
                    .ToList();

                foreach (RentableItemsField field in fields)
                {
                    RentableItemsFieldUpdateModel fieldModel = rentableItemsFieldsModel.Fields.FirstOrDefault(x => x.Id == field.FieldId);
                    if (fieldModel != null)
                    {
                        field.FieldStatus = fieldModel.FieldStatus;
                    }
                }

                _dbContext.SaveChanges();
                return new OperationResult(true,
                    _localizationService.GetString("FieldsUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false,
                    _localizationService.GetString("ErrorWhileUpdatingFields"));
            }
        }
    }
}
