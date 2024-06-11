using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.Common.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        public string Source { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Source} - {Description}";
        }
    }

    [ExcludeFromCodeCoverage]
    public class ErrorDescription
    {
        public string CorrelationId { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
