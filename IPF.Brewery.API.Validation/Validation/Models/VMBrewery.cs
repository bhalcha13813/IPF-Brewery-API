using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Validation.Models
{
    [ExcludeFromCodeCoverage]
    public class VMBrewery
    {
        public VMBrewery()
        {
            
        }

        public int? Id { get; set; }
        public string BreweryName { get; set; }
        public string Address { get; set; }
    }
}
