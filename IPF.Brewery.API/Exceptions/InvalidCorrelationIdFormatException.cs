using System.Runtime.Serialization;

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

        protected InvalidCorrelationIdFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
