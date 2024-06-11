using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BeerResponseModel
    {
        public int Id { get; set; }
        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public string BeerType { get; set; }
    }
}
