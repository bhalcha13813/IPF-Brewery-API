using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Validation.Models
{
    [ExcludeFromCodeCoverage]
    public class VMBar
    {
        public VMBar()
        {
            
        }

        public int? Id { get; set; }
        public string BarName { get; set; }
        public string Address { get; set; }
    }
}
