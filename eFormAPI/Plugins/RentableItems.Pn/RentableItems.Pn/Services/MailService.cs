using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormRentableItemBase.Infrastructure.Data;
using Microting.eFormRentableItemBase.Infrastructure.Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RentableItems.Pn.Abstractions;
using RentableItems.Pn.Infrastructure.Models;

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
            try
            {
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "Mathias Husum Nielsen",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    // Console.WriteLine("Credential file saved to", credPath);
                }

                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                var emailListRequest = service.Users.Messages.List("mhn@microting.dk");
                emailListRequest.LabelIds = "INBOX";
                emailListRequest.IncludeSpamTrash = false;
                emailListRequest.Q = "from:rm@microting.dk";
                emailListRequest.MaxResults = 1;

                var emailListResponse = await emailListRequest.ExecuteAsync();

                if (emailListResponse != null && emailListResponse.Messages != null)
                {
                    foreach (var email in emailListResponse.Messages)
                    {
                        var emailInfoRequest = service.Users.Messages.Get("mhn@microting.dk", email.Id);
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
                                        .Get("mhn@microting.dk", email.Id, attId).Execute();

                                    string attachData = attachPart.Data.Replace("-", "+");
                                    attachData = attachData.Replace("_", "/");

                                    byte[] data = Convert.FromBase64String(attachData);
                                    // File.WriteAllBytes(Path.Combine("", part.Filename), data);

                                    var bytesAsString = Encoding.Default.GetString(data);
                                    // var jsonText = JsonConvert.SerializeObject(bytesAsString);
                                    //
                                    // JToken rawJson = JToken.Parse(jsonText);
                                    // Console.WriteLine(rawJson);


                                    bytesAsString = bytesAsString.Replace(@"""", @"");
                                    string[] rawFile = bytesAsString.Split("\n");

                                    var importList = rawFile.Skip(2);

                                    string[] list = importList.ToArray();

                                    var headers = rawFile[0].Split(",");
                                    
                                    list = list[0].Split(",");
                                    
                                    Contract contract = null;
                                    RentableItem rentableItem = null;
                                    Customer customer = null;

                                    foreach (var header in headers)
                                    {
                                        if (header == "")
                                        {
                                         
                                            contract = new Contract
                                            {
                                                ContractNr = int.Parse(list[0]),
                                                // ContractStart = importList[6],
                                                // ContractEnd = importList[7],
                                            };
                                            // if (contract.ContractNr != int.Parse(importList[0]))
                                            // {
                                            // await contract.Create(_dbContext);
                                            // }   
                                        }

                                        if (header == ".")
                                        {
                                         
                                            customer = new Customer
                                            {
                                                // CompanyName = importList[1]
                                            };
                                            // if (customer.CompanyName != importList[1])
                                            // {
                                            // await customer.Create(_customerDbContext);
                                            // }   
                                        }

                                        if (header == "ok")
                                        {
                                            rentableItem = new RentableItem
                                            {
                                                // Brand = importList[2],
                                                // ModelName = importList[3],
                                                // RegistrationDate = (DateTime)int.Parse(importList[4]),
                                                // VinNumber = importList[5]
                                            };
                                            // if (rentableItem.Brand != importList[2] &&
                                            // rentableItem.ModelName != importList[3] &&
                                            // rentableItem.VinNumber != importList[5])
                                            // {
                                            // await rentableItem.Create(_dbContext);
                                            // }    
                                        }
                                        
                                        ContractRentableItem contractRentableItem = null;
                                        if (rentableItem.Id != null && contract.Id != null)
                                        {
                                            contractRentableItem = new ContractRentableItem
                                            {
                                                ContractId = contract.Id,
                                                RentableItemId = rentableItem.Id
                                            };
                                            // await contractRentableItem.Create(_dbContext);
                                        } 
                                    }
                                       
                                    // await _importsService.Import(importModel);
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