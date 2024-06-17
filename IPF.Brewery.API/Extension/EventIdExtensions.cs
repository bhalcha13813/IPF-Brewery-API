using IPF.Brewery.Common.Logging;

namespace IPF.Brewery.API.Extension
{
    public static class EventIdExtensions
    {
        public static EventId ToEventId(this EventIds eventId)
        {
            return new EventId((int)eventId, eventId.ToString());
        }
    }
}
