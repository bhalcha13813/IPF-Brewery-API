using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BreweryResponseModel
    {
        public int Id { get; set; }
        public string BreweryName { get; set; }
        public string Address { get; set; }
    }
}
