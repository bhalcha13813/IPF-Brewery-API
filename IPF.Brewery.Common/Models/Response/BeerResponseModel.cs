namespace IPF.Brewery.Common.Models.Response
{
    public class BeerResponseModel
    {
        public int Id { get; set; }
        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public string BeerType { get; set; }
    }
}
