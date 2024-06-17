using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Service;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Logging;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.Controllers
{
    public class BreweryController : BaseController<BreweryController>
    {
        private readonly ILogger<BreweryController> logger;
        private readonly IBreweryService breweryService; 
        public BreweryController(IHttpContextAccessor httpContextAccessor,
                                 ILogger<BreweryController> logger,
                                 IBreweryService breweryService ) : base(httpContextAccessor, logger)
        {
            this.logger = logger;
            this.breweryService = breweryService;
        }

        [HttpGet]
        [Route("/brewery")]
        public IActionResult GetBreweries()
        { 
           logger.LogInformation(EventIds.BreweriesRetrievalStarted.ToEventId(), "Retrieval of breweries started.");

           List<BreweryResponseModel> breweries = breweryService.GetBreweries();
           
           logger.LogInformation(EventIds.BreweriesDetailsRetrieved.ToEventId(), "Retrieval of breweries completed.");

            return new OkObjectResult(breweries);
        }

        [HttpGet]
        [Route("/brewery/{breweryId}")]
        public IActionResult GetBrewery(int breweryId)
        {
            logger.LogInformation(EventIds.BreweryRetrievalStarted.ToEventId(), "Retrieval of brewery started.");

            BreweryResponseModel? brewery = breweryService.GetBrewery(breweryId);
            
            logger.LogInformation(EventIds.BreweryDetailsRetrieved.ToEventId(), "Retrieval of brewery completed.");

            return new OkObjectResult(brewery);
        }

        [HttpGet]
        [Route("/brewery/{breweryId}/beer")]
        public IActionResult GetBreweryBeers(int breweryId)
        {
            logger.LogInformation(EventIds.BreweryBeersRetrievalStarted.ToEventId(), "Retrieval of brewery-beers for {BreweryId} started.", breweryId);

            BreweryBeer? breweryBeers = breweryService.GetBreweryBeers(breweryId);
            
            logger.LogInformation(EventIds.BreweryBeersRetrieved.ToEventId(), "Retrieval of brewery-beers for {BreweryId} completed.", breweryId);

            return new OkObjectResult(breweryBeers);
        }

        [HttpGet]
        [Route("/brewery/beer")]
        public IActionResult getAllBreweriesWithBeers()
        {
            logger.LogInformation(EventIds.BreweryBeersRetrievalStarted.ToEventId(), "Retrieval of brewery-beers started.");

            List<BreweryBeer> allbreweriesWithBeers = breweryService.GetAllBreweriesWithBeers();
            
            logger.LogInformation(EventIds.BreweryBeersRetrievalStarted.ToEventId(), "Retrieval of brewery-beers completed.");

            return new OkObjectResult(allbreweriesWithBeers);
        }

        [HttpPost]
        [Route("/brewery")]
        public IActionResult AddBrewery(BreweryPayload breweryPayload)
        {
            logger.LogInformation(EventIds.AddBreweryStarted.ToEventId(), "Add new brewery started.");

            VMBrewery vmBrewery = new VMBrewery() { BreweryName = breweryPayload.BreweryName };
            ValidationResult validationResult = breweryService.ValidateBrewery(vmBrewery);

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

            breweryService.AddBrewery(breweryPayload);
            
            logger.LogInformation(EventIds.AddBreweryCompleted.ToEventId(), "Add new brewery completed.");
            
            return new OkResult();
        }

        [HttpPut]
        [Route("/brewery/{breweryId}")]
        public IActionResult UpdateBrewery(int breweryId, BreweryPayload breweryPayload)
        {
            logger.LogInformation(EventIds.UpdateBreweryStarted.ToEventId(), "Update brewery {BreweryId} started.", breweryId);

            VMBrewery vmBrewery = new VMBrewery() { Id = breweryId, BreweryName = breweryPayload.BreweryName };
            ValidationResult validationResult = breweryService.ValidateBrewery(vmBrewery);

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

            breweryService.UpdateBrewery(breweryId, breweryPayload);
            
            logger.LogInformation(EventIds.UpdateBreweryCompleted.ToEventId(), "Update brewery {BreweryId} completed.", breweryId);
            
            return new OkResult();
        }

        [HttpPost]
        [Route("/brewery/beer")]
        public IActionResult AddBreweryBeer(BreweryBeerPayload breweryBeerPayload)
        {
            logger.LogInformation(EventIds.MapBreweryBeerStarted.ToEventId(), "Map brewery {BreweryId} beer {BeerId} started.", breweryBeerPayload.BreweryId, breweryBeerPayload.BeerId);

            VMBreweryBeer vmBreweryBeer = new VMBreweryBeer() { BreweryId = breweryBeerPayload.BreweryId, BeerId = breweryBeerPayload.BeerId };
            ValidationResult validationResult = breweryService.ValidateBreweryBeer(vmBreweryBeer);

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
            
            breweryService.AddBreweryBeer(breweryBeerPayload);
            
            logger.LogInformation(EventIds.MapBreweryBeerCompleted.ToEventId(), "Map brewery {BreweryId} beer {BeerId} completed.", breweryBeerPayload.BreweryId, breweryBeerPayload.BeerId);
            
            return new OkResult();
        }
    }
}
