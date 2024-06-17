using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Filters;
using IPF.Brewery.Common.Logging;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.ConstrainedExecution;

namespace IPF.Brewery.API.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        protected readonly ILogger<T> logger;
        protected new HttpContext HttpContext => httpContextAccessor.HttpContext;

        protected BaseController(IHttpContextAccessor httpContextAccessor, ILogger<T> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        protected string GetCurrentCorrelationId()
        {
            return httpContextAccessor.HttpContext.Request.Headers[CorrelationIdMiddleware.XCorrelationIdHeaderKey].FirstOrDefault();
        }

        protected IActionResult BuildConflictErrorResponse(List<Error> errors)
        {
            logError(EventIds.BadRequest.ToEventId(), errors, "Conflict");
            return new ConflictObjectResult(new ErrorDescription
            {
                Errors = errors,
                CorrelationId = GetCurrentCorrelationId()
            });
        }

        protected IActionResult BuildBadRequestErrorResponse(List<Error> errors)
        {
            logError(EventIds.BadRequest.ToEventId(), errors, "BadRequest");
            return new BadRequestObjectResult(new ErrorDescription
            {
                Errors = errors,
                CorrelationId = GetCurrentCorrelationId()
            });
        }

        private void logError(EventId eventId, List<Error> errors, string errorType)
        { 
            logger.LogError(eventId, $"{HttpContext.Request.Path} - {errorType} - {{Errors}}", errors);
        }
    }
}
