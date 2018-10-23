namespace RentableItems.Pn.Abstractions
{
    public interface IRentableItemsLocalizationService
    {
        string GetString(string key);
        string GetString(string format, params object[] args);
    }
}