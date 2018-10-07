using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Localization;

namespace Vehicles.Pn.Resources
{
    public class VehicleLocalizationService : IVehicleLocalizationService
    {
        private readonly IStringLocalizer _localizer;
 
        public VehicleLocalizationService(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("VehicleResources",
                Assembly.GetEntryAssembly().FullName);
        }
 
        public string GetString(string key)
        {
            var cultureInfo = new CultureInfo("da");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
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
