namespace Vehicles.Pn.Resources
{
    public interface IVehicleLocalizationService
    {
        string GetString(string key);
        string GetString(string format, params object[] args);
    }
}