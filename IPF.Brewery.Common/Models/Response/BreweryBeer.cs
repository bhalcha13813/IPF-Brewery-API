using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BreweryBeer
    {
        public BreweryResponseModel Brewery { get; set; }
        public ICollection<BeerResponseModel> Beers { get; set; }
    }
}
