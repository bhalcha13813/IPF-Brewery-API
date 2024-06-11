using IPF.Brewery.API.Filters;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        protected new HttpContext HttpContext => httpContextAccessor.HttpContext;

        protected BaseController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected string GetCurrentCorrelationId()
        {
            return httpContextAccessor.HttpContext.Request.Headers[CorrelationIdMiddleware.XCorrelationIdHeaderKey].FirstOrDefault();
        }

        protected IActionResult BuildConflictErrorResponse(List<Error> errors, string batchId = null)
        {
            //LogError(EventIds.Conflict.ToEventId(), errors, "Conflict", batchId);

            return new ConflictObjectResult(new ErrorDescription
            {
                Errors = errors,
                CorrelationId = GetCurrentCorrelationId()
            });
        }

        protected IActionResult BuildBadRequestErrorResponse(List<Error> errors, string batchId = null)
        {
            //LogError(EventIds.BadRequest.ToEventId(), errors, "BadRequest", batchId);

            return new BadRequestObjectResult(new ErrorDescription
            {
                Errors = errors,
                CorrelationId = GetCurrentCorrelationId()
            });
        }
    }
}
