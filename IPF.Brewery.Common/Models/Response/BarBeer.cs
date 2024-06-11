namespace IPF.Brewery.Common.Models.Response
{
    public class BarBeer
    {
        public BarResponseModel Bar { get; set; }
        public ICollection<BeerResponseModel> Beers { get; set; }
    }
}
