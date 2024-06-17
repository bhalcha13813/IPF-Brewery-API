using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Validation.Models
{
    [ExcludeFromCodeCoverage]
    public class VMBreweryBeer
    {
        public VMBreweryBeer()
        {
            
        }

        public int BreweryId { get; set; }
        public int BeerId { get; set; }
    }
}
