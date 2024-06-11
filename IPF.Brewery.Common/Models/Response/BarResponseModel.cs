using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BarResponseModel
    {
        public BarResponseModel()
        {
           
        }

        public int Id { get; set; }
        public string BarName { get; set; }
        public string Address { get; set; }
    }
}
