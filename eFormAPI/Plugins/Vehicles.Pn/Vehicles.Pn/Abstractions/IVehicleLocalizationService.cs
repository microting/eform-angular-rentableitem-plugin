namespace Vehicles.Pn.Abstractions
{
    public interface IVehicleLocalizationService
    {
        string GetString(string key);
        string GetString(string format, params object[] args);
    }
}