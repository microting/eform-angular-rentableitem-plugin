namespace RentableItems.Pn.Infrastructure.Models
{
    public class RentableItemBaseSettings
    {
        public string LogLevel { get; set; }
        public string LogLimit { get; set; }
        public string SdkConnectionString { get; set; }
        public string MaxParallelism { get; set; }
        public int NumberOfWorkers { get; set; }
        public string EnabledSiteIds { get; set; }
        public string SdkeFormId { get; set; }
        public string GmailCredentials { get; set; }
        public string GmailClientSecret { get; set; }
        public string GmailEmail { get; set; }
        public string GmailUserName { get; set; }
        public string MailFrom { get; set; }
        public string Token { get; set; }
    }
}