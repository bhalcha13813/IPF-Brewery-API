using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class BreweryPayload
    {
        public string BreweryName { get; set; }
        public string Address { get; set; }
    }
}
