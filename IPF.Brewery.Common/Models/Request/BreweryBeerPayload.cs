using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class BreweryBeerPayload
    {
        public BreweryBeerPayload()
        {
            
        }

        public int BreweryId { get; set; }
        public int BeerId { get; set; }
    }
}
