using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Validation.Models
{
    [ExcludeFromCodeCoverage]
    public class VMBeerType
    {
        public VMBeerType()
        {
            
        }

        public int? Id { get; set; }
        public string BeerType { get; set; }
    }
}
