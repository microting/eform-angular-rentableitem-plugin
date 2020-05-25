using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Json;
using Microsoft.EntityFrameworkCore;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using RentableItems.Pn.Abstractions;

namespace RentableItems.Pn.Services
{
    public class MailService : IMailService
    {

        private readonly IImportsService _importsService;
        private readonly eFormRentableItemPnDbContext _dbContext;
        private readonly CustomersPnDbAnySql _customerDbContext;
        
        public MailService(IImportsService importsService,
            eFormRentableItemPnDbContext dbContext,
            CustomersPnDbAnySql customerDbContext)
        {
            _importsService = importsService;
            _dbContext = dbContext;
            _customerDbContext = customerDbContext;
        }
        
        private string[] Scopes = {GmailService.Scope.GmailReadonly};
        private string ApplicationName = "Gmail API .NET Quickstart";

        public async Task<OperationResult> Read()
        {
            UserCredential credential;
            var pluginConfiguration = await _dbContext.PluginConfigurationValues.SingleOrDefaultAsync(x => x.Name == "RentableItemBaseSettings:GmailCredentials");
            GoogleClientSecrets secrets = NewtonsoftJsonSerializer.Instance.Deserialize<GoogleClientSecrets>(pluginConfiguration.Value);
            pluginConfiguration = await _dbContext.PluginConfigurationValues.SingleOrDefaultAsync(x => x.Name == "RentableItemBaseSettings:GmailUserName");
            string gmailUserName = pluginConfiguration.Value;
            pluginConfiguration = await _dbContext.PluginConfigurationValues.SingleOrDefaultAsync(x => x.Name == "RentableItemBaseSettings:MailFrom");
            string mailFrom = pluginConfiguration.Value;
            pluginConfiguration = await _dbContext.PluginConfigurationValues.SingleOrDefaultAsync(x => x.Name == "RentableItemBaseSettings:GmailEmail");
            string gmailEmail = pluginConfiguration.Value;
            pluginConfiguration = await _dbContext.PluginConfigurationValues.SingleOrDefaultAsync(x => x.Name == "RentableItemBaseSettings:SdkeFormId");
            int eFormId = int.Parse(pluginConfiguration.Value);

            try
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets.Secrets,
                    Scopes,
                    gmailUserName,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                var emailListRequest = service.Users.Messages.List(gmailEmail);
                emailListRequest.LabelIds = "INBOX";
                emailListRequest.IncludeSpamTrash = false;
                emailListRequest.Q = $"from:{mailFrom}";
                emailListRequest.MaxResults = 1;

                var emailListResponse = await emailListRequest.ExecuteAsync();

                if (emailListResponse != null && emailListResponse.Messages != null)
                {
                    foreach (var email in emailListResponse.Messages)
                    {
                        var emailInfoRequest = service.Users.Messages.Get(gmailEmail, email.Id);
                        var emailInfoResponse = await emailInfoRequest.ExecuteAsync();
                        if (emailInfoResponse != null)
                        {
                            IList<MessagePart> parts = emailInfoResponse.Payload.Parts;
                            foreach (var part in parts)
                            {
                                if (!string.IsNullOrEmpty(part.Filename))
                                {
                                    string attId = part.Body.AttachmentId;
                                    MessagePartBody attachPart = service.Users.Messages.Attachments
                                        .Get(gmailEmail, email.Id, attId).Execute();

                                    string attachData = attachPart.Data.Replace("-", "+");
                                    attachData = attachData.Replace("_", "/");

                                    byte[] data = Convert.FromBase64String(attachData);

                                    var bytesAsString = Encoding.Default.GetString(data);
                                    string[] rawFile = bytesAsString.Split("\n");

                                    var importList = rawFile.Skip(2);

                                    string[] list = importList.ToArray();

                                    var headers = rawFile[0].Split(",");

                                    foreach (string s in list)
                                    {
                                        string[] lSplit = s.Split("\",\"");
                                        if (lSplit.Length == 8)
                                        {
                                            Contract contract = null;
                                            Customer customer = null;
                                            ContractRentableItem contractRentableItem = null;
                                            try
                                            {
                                                customer = _customerDbContext.Customers.SingleOrDefault(x => x.CompanyName == lSplit[1] || x.ContactPerson == lSplit[1]);
                                                if (customer == null)
                                                {
                                                    customer = new Customer()
                                                    {
                                                        CompanyName = lSplit[1]
                                                    };
                                                    await customer.Create(_customerDbContext);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e);
                                                throw;
                                            }

                                            RentableItem rentableItem = null;
                                            try
                                            {
                                                rentableItem = _dbContext.RentableItem.SingleOrDefault(x => x.Brand == lSplit[2] && x.ModelName == lSplit[3] && x.RegistrationDate == DateTime.Parse(lSplit[4]));
                                                if (rentableItem == null)
                                                {
                                                    rentableItem = new RentableItem()
                                                    {
                                                        Brand = lSplit[2],
                                                        ModelName = lSplit[3],
                                                        RegistrationDate = DateTime.Parse(lSplit[4]),
                                                        VinNumber = lSplit[5],
                                                        eFormId = eFormId
                                                    };
                                                    await rentableItem.Create(_dbContext);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e);
                                                throw;
                                            }

                                            try
                                            {
                                                contract = _dbContext.Contract.SingleOrDefault(x =>
                                                    x.ContractNr == int.Parse(lSplit[0].Replace("\"", "")));
                                                if (contract == null)
                                                {
                                                    contract = new Contract()
                                                    {
                                                        ContractNr = int.Parse(lSplit[0].Replace("\"", "")),
                                                        ContractStart = DateTime.Parse(lSplit[6]),
                                                        ContractEnd = DateTime.Parse(lSplit[7].Replace("\r", "").Replace("\"", "")),
                                                        CustomerId = customer.Id,
                                                        Status = 0
                                                    };
                                                    await contract.Create(_dbContext);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e);
                                                throw;
                                            }

                                            try
                                            {
                                                contractRentableItem =
                                                    _dbContext.ContractRentableItem.SingleOrDefault(x =>
                                                        x.ContractId == contract.Id &&
                                                        x.RentableItemId == rentableItem.Id);
                                                if (contractRentableItem == null)
                                                {
                                                    contractRentableItem = new ContractRentableItem()
                                                    {
                                                        ContractId = contract.Id,
                                                        RentableItemId = rentableItem.Id
                                                    };
                                                    await contractRentableItem.Create(_dbContext);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e);
                                                throw;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}