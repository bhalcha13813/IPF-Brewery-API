using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Validation.Models
{
    [ExcludeFromCodeCoverage]
    public class VMBarBeer
    {
        public VMBarBeer()
        {
            
        }

        public int BarId { get; set; }
        public int BeerId { get; set; }
    }
}
