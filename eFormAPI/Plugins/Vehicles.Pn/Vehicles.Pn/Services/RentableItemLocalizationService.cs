using System.Reflection;
using Microsoft.Extensions.Localization;
using RentableItems.Pn.Abstractions;

namespace RentableItems.Pn.Services
{
    public class RentableItemLocalizationService : IRentableItemsLocalizationService
    {
        private readonly IStringLocalizer _localizer;
 
        public RentableItemLocalizationService(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("VehicleResources",
                Assembly.GetEntryAssembly().FullName);
        }
 
        public string GetString(string key)
        {
            var str = _localizer[key];
            return str.Value;
        }

        public string GetString(string format, params object[] args)
        {
            var message = _localizer[format];
            if (message?.Value == null)
            {
                return null;
            }

            return string.Format(message.Value, args);
        }
    }
}
