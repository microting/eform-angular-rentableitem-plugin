using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;

namespace RentableItems.Pn.Controllers
{
    public class MailController : Controller
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        [Route("api/mail/get")]
        public async Task<OperationResult> Read(string body)
        {
            return await _mailService.Read();
        }
        
    }
}