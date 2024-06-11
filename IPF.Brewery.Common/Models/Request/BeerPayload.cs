namespace IPF.Brewery.Common.Models.Request
{
    public class BeerPayload
    {
        public BeerPayload()
        {
            
        }

        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public int BeerTypeId { get; set; }
    }
}
