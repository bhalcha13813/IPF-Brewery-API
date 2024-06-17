using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class BarBeerPayload
    {
        public int BarId { get; set; }
        public int BeerId { get; set; }
    }
}
