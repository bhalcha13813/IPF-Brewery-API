using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Validation.Models
{
    [ExcludeFromCodeCoverage]
    public class VMBeer
    {
        public VMBeer()
        {
            
        }

        public int? Id { get; set; }
        public string BeerName { get; set; }
        public int BeerTypeId { get; set; }

    }
}
