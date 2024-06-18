namespace IPF.Brewery.API.Exceptions
{
    /// <summary>
    /// An exception thrown when an Correlation Id is not Guid.
    /// </summary>
    [Serializable]
    public class InvalidCorrelationIdFormatException : Exception
    {
        public InvalidCorrelationIdFormatException(string message)
             : base(message)
        {
        }
    }
}
