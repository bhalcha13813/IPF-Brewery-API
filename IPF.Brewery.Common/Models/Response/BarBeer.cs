using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BarBeer
    {
        public BarResponseModel Bar { get; set; }
        public ICollection<BeerResponseModel> Beers { get; set; }
    }
}
