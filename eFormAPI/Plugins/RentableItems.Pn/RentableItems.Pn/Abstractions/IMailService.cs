using System.Threading.Tasks;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace RentableItems.Pn.Abstractions
{
    public interface IMailService
    {
        Task<OperationResult> Read();
    }
}