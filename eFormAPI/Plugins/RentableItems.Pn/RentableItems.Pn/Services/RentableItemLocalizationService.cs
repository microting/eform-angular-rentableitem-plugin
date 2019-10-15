using RentableItems.Pn.Abstractions;
using Microsoft.Extensions.Localization;
using Microting.eFormApi.BasePn.Localization.Abstractions;

namespace RentableItems.Pn.Services
{
    public class RentableItemLocalizationService : IRentableItemsLocalizationService
    {
        private readonly IStringLocalizer _localizer;
 
        public RentableItemLocalizationService(IEformLocalizerFactory factory)
        {
            _localizer = factory.Create(typeof(EformRentableItemsPlugin));
        }
 
        public string GetString(string key)
        {
            LocalizedString str = _localizer[key];
            return str.Value;
        }

        public string GetString(string format, params object[] args)
        {
            LocalizedString message = _localizer[format];
            if (message?.Value == null)
            {
                return null;
            }

            return string.Format(message.Value, args);
        }
    }
}
