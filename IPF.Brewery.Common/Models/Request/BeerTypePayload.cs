using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class BeerTypePayload
    {
        public BeerTypePayload()
        {
            
        }

        public string BeerType { get; set; }
    }
}
