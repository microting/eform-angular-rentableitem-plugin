using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using RentableItems.Pn.Abstractions;

namespace RentableItems.Pn.Controllers
{
    [Route("/api/rentable-items-pn/mail")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet]
        [Route("{token}")]
        [AllowAnonymous]
        public async Task<OperationResult> Read(string token)
        {
            return await _mailService.Read(token);
        }
        
    }
}