using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class BarPayload
    {
        public string BarName{ get; set; }
        public string Address { get; set; }
    }
}
