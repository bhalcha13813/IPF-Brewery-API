using System.Diagnostics.CodeAnalysis;
using IPF.Brewery.API.Exceptions;

namespace IPF.Brewery.API.Filters
{
    [ExcludeFromCodeCoverage]
    public static class CorrelationIdMiddleware
    {
        public const string XCorrelationIdHeaderKey = "X-Correlation-ID";

        public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder builder, ILoggerFactory loggerFactory)
        {
            Guid xCorrelationId;

            return builder.Use(async (context, func) =>
            {
                var correlationId = context.Request.Headers[XCorrelationIdHeaderKey].FirstOrDefault();

                if (string.IsNullOrEmpty(correlationId))
                {
                    correlationId = Guid.NewGuid().ToString();
                    context.Request.Headers.Add(XCorrelationIdHeaderKey, correlationId);
                }

                if (Guid.TryParse(correlationId, out xCorrelationId))
                {
                    correlationId = xCorrelationId.ToString();

                    context.Response.Headers.Add(XCorrelationIdHeaderKey, correlationId);

                    await func();

                }
                else
                {
                    try
                    {
                        throw (new InvalidCorrelationIdFormatException($"Invalid X-Correlation-ID {correlationId} format exception."));

                    }
                    catch (InvalidCorrelationIdFormatException e)
                    {
                        loggerFactory
                        .CreateLogger(context.Request.Path)
                            .LogError("Invalid X-Correlation-ID format exception {Exception}", e);
                        context.Response.StatusCode = 400;
                        await HttpResponseWritingExtensions.WriteAsync(context.Response, "Invalid X-Correlation-ID format exception, it should be GUID.");
                    }
                }

            });
        }

    }
}
