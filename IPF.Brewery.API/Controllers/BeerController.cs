using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Service;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Logging;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.Controllers
{
    public class BeerController : BaseController<BeerController>
    {
        private readonly ILogger<BeerController> logger;
        private readonly IBeerService beerService;
        public BeerController(IHttpContextAccessor contextAccessor,
                              ILogger<BeerController> logger,
                              IBeerService beerService) : base(contextAccessor, logger)
        {
            this.logger = logger;
            this.beerService = beerService;
        }

        [HttpGet]
        [Route("/Beer/{beerId}")]
        public ActionResult<BeerResponseModel?> GetBeer(int beerId)
        {
            logger.LogInformation(EventIds.BeerRetrievalStarted.ToEventId(), "Retrieval of beer {BeerId} started.", beerId);
            
            BeerResponseModel? beer = beerService.GetBeer(beerId);
            
            logger.LogInformation(EventIds.BeerDetailsRetrieved.ToEventId(), "Retrieval of beer {BeerId} completed.", beerId);

            return new OkObjectResult(beer);
        }

        [HttpGet]
        [Route("/Beer/All")]
        public ActionResult<List<BeerResponseModel>> GetBeers()
        {
            logger.LogInformation(EventIds.BeersRetrievalStarted.ToEventId(), "Retrieval of beers started.");
            
            List<BeerResponseModel> beers = beerService.GetBeers();

            logger.LogInformation(EventIds.BeersRetrievalCompleted.ToEventId(), "Retrieval of bars completed.");

            return new OkObjectResult(beers);
        }

        [HttpGet]
        [Route("/Beer")]
        public ActionResult<List<BeerResponseModel>> GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            logger.LogInformation(EventIds.BeersRetrievalStarted.ToEventId(), "Retrieval of beers within alcohol percentage range started.");

            List<BeerResponseModel> beers = beerService.GetBeers(gtAlcoholByVolume, ltAlcoholByVolume);
            
            logger.LogInformation(EventIds.BeersRetrievalCompleted.ToEventId(), "Retrieval of beers within alcohol percentage range completed.");
            
            return new OkObjectResult(beers);
        }

        [HttpPost]
        [Route("/Beer")]
        public IActionResult AddBeer(BeerPayload beerPayload)
        {
            logger.LogInformation(EventIds.AddBeerStarted.ToEventId(), "Add new beer started.");

            VMBeer vmBeer = new VMBeer() { BeerName = beerPayload.BeerName, BeerTypeId = beerPayload.BeerTypeId};
            ValidationResult validationResult = beerService.ValidateBeer(vmBeer);

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
            beerService.AddBeer(beerPayload);

            logger.LogInformation(EventIds.AddBeerCompleted.ToEventId(), "Add new beer completed.");

            return new OkResult();
        }

        [HttpPut]
        [Route("/Beer/{beerId}")]
        public IActionResult UpdateBeer(int beerId, BeerPayload beerPayload)
        {
            logger.LogInformation(EventIds.UpdateBeerStarted.ToEventId(), "Update beer {BeerId} started.", beerId);

            VMBeer vmBeer = new VMBeer() { Id = beerId, BeerName = beerPayload.BeerName, BeerTypeId = beerPayload.BeerTypeId };
            ValidationResult validationResult = beerService.ValidateBeer(vmBeer);

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
            
            beerService.UpdateBeer(beerId, beerPayload);

            logger.LogInformation(EventIds.UpdateBeerCompleted.ToEventId(), "Update bar {BeerId} started.", beerId);
            
            return new OkResult();
        }
    }
}
