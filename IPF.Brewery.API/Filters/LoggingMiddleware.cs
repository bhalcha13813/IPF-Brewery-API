using System.Diagnostics.CodeAnalysis;

namespace IPF.Brewery.API.Filters
{
    public static class LoggingMiddleware
    {
        [ExcludeFromCodeCoverage] //Used in Startup.cs
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder appBuilder, ILoggerFactory loggerFactory)
        {
            return appBuilder.Use(async (context, func) =>
            {
                try
                {
                    await func();
                }
                catch (Exception e)
                {
                    loggerFactory
                        .CreateLogger(context.Request.Path)
                        .LogError("Unhandled controller exception {Exception}", e);
                    context.Response.StatusCode = 500;

                }
            });
        }

    }
}
