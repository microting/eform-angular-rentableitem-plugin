using System.Threading;
using Vehicles.Pn.Properties;

namespace Vehicles.Pn.Helpers
{
    public static class VehiclePnLocaleHelper
    {
        public static string GetString(string str)
        {
            var message = VehiclePnResources.ResourceManager.GetString(str, Thread.CurrentThread.CurrentCulture);
            return message;
        }

        public static string GetString(string format, params object[] args)
        {
            var message = VehiclePnResources.ResourceManager.GetString(format, Thread.CurrentThread.CurrentCulture);
            if (message == null)
            {
                return null;
            }
            message = string.Format(message, args);
            return message;
        }
    }
}
