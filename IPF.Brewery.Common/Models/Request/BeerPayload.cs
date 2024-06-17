using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class BeerPayload
    {
        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public int BeerTypeId { get; set; }
    }
}
