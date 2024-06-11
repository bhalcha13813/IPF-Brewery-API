namespace IPF.Brewery.Common.Models.Response
{
    public class BreweryBeer
    {
        public BreweryResponseModel Brewery { get; set; }
        public ICollection<BeerResponseModel> Beers { get; set; }
    }
}
