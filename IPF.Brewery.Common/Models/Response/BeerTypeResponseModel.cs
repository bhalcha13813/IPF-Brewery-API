using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BeerTypeResponseModel
    {
        public int Id { get; set; }
        public string BeerType { get; set; }

    }
}
