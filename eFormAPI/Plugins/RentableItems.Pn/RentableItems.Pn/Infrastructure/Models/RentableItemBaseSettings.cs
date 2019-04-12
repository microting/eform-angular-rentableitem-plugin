namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemBaseSettings
    {
        public string LogLevel { get; set; }
        public string LogLimit { get; set; }
        public string SdkConnectionString { get; set; }
        public string MaxParallelism { get; set; }
        public int NumberOfWorkers { get; set; }
        public string SdkeFormId { get; set; }

    }
}