using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Service;
using IPF.Brewery.Common.Logging;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;
using VMBeerType = IPF.Brewery.API.Validation.Models.VMBeerType;

namespace IPF.Brewery.API.Controllers
{
    public class BeerTypeController : BaseController<BeerTypeController>
    {
        private readonly ILogger<BeerTypeController> logger;
        private readonly IBeerTypeService beerTypeService;
        public BeerTypeController(IHttpContextAccessor contextAccessor,
                                  ILogger<BeerTypeController> logger,
                                  IBeerTypeService beerTypeService) : base(contextAccessor, logger)
        {
            this.logger = this.logger;
            this.beerTypeService = beerTypeService;
        }


        [HttpGet]
        [Route("/BeerType")]
        public IActionResult GetBeerTypes()
        {
            logger.LogInformation(EventIds.BeerTypesRetrievalStarted.ToEventId(), "Retrieval of beer types started.");

            List<BeerTypeResponseModel> beerTypes = beerTypeService.GetBeerTypes();

            logger.LogInformation(EventIds.BeerTypesRetrievalCompleted.ToEventId(), "Retrieval of beer types completed.");

            return new OkObjectResult(beerTypes);
        }

        [HttpPost]
        [Route("/BeerType")]
        public IActionResult AddBeerType(BeerTypePayload beerTypePayload)
        {
            logger.LogInformation(EventIds.AddBeerTypeStarted.ToEventId(), "Add new beer type started.");

            VMBeerType beerType = new VMBeerType() {BeerType = beerTypePayload.BeerType};
            ValidationResult validationResult = beerTypeService.ValidateBeerType(beerType);

            if (!validationResult.IsValid)
            {
                List<Error> errors;

                if (validationResult.HasConflictErrors(out errors))
                {
                    return BuildConflictErrorResponse(errors);
                }

                if (validationResult.HasBadRequestErrors(out errors))
                {
                    return BuildBadRequestErrorResponse(errors);
                }
            }
            
            beerTypeService.AddBeerType(beerTypePayload);

            logger.LogInformation(EventIds.AddBeerTypeCompleted.ToEventId(), "Add new beer type completed.");

            return new OkResult();
        }

        [HttpPut]
        [Route("/BeerType/{beerTypeId}")]
        public IActionResult UpdateBeer(int beerTypeId, BeerTypePayload beerTypePayload)
        {
            logger.LogInformation(EventIds.UpdateBeerTypeStarted.ToEventId(), "Update beer type {BeerTypeId} started.", beerTypeId);

            VMBeerType beerType = new VMBeerType() { Id = beerTypeId ,BeerType = beerTypePayload.BeerType };
            ValidationResult validationResult = beerTypeService.ValidateBeerType(beerType);

            if (!validationResult.IsValid)
            {
                List<Error> errors;

                if (validationResult.HasConflictErrors(out errors))
                {
                    return BuildConflictErrorResponse(errors);
                }

                if (validationResult.HasBadRequestErrors(out errors))
                {
                    return BuildBadRequestErrorResponse(errors);
                }
            }

            beerTypeService.UpdateBeerType(beerTypeId, beerTypePayload);

            logger.LogInformation(EventIds.UpdateBeerTypeCompleted.ToEventId(), "Update beer type {BeerTypeId} completed.", beerTypeId);

            return new OkResult();
        }
    }
}
