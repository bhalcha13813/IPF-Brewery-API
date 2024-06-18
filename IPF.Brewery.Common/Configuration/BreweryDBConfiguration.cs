namespace IPF.Brewery.Common.Configuration
{
    public class BreweryDBConfiguration
    {
        public string ConnectionString { get; set; }

        public int MaxRetryCount { get; set; }

        public int MaxRetryDelayInSeconds { get; set; }
    }
}
