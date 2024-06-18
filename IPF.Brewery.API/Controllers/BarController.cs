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
    public class BarController : BaseController<BarController>
    {
        private readonly ILogger<BarController> logger;
        private readonly IBarService barService; 

        public BarController(IHttpContextAccessor contextAccessor,
                             ILogger<BarController> logger,
                             IBarService barService) : base(contextAccessor, logger)
        {
            this.logger = logger;
            this.barService = barService;
        }


        [HttpGet]
        [Route("/bar")]
        public ActionResult<List<BarResponseModel>> GetBars()
        {
            logger.LogInformation(EventIds.BarsRetrievalStarted.ToEventId(), "Retrieval of bars started.");

            List<BarResponseModel> bars = barService.GetBars();
            
            logger.LogInformation(EventIds.BarsDetailsRetrieved.ToEventId(), "Retrieval of bars completed.");

            return new OkObjectResult(bars);
        }

        [HttpGet]
        [Route("/bar/{barId}")]
        public ActionResult<BarResponseModel> GetBar(int barId)
        {
            logger.LogInformation(EventIds.BarRetrievalStarted.ToEventId(), "Retrieval of bar {BarId} started.", barId);
            
            BarResponseModel? bar = barService.GetBar(barId);
            
            logger.LogInformation(EventIds.BarDetailsRetrieved.ToEventId(), "Retrieval of bar {BarId} completed.", barId);
            
            return new OkObjectResult(bar);
        }

        [HttpGet]
        [Route("/bar/{barId}/beer")]
        public ActionResult<BarBeer?> GetBarBeers(int barId)
        {
            logger.LogInformation(EventIds.BarBeersRetrievalStarted.ToEventId(), "Retrieval of bar-beers for {BarId} started.", barId);
            
            BarBeer? barBeers = barService.GetBarBeers(barId);

            logger.LogInformation(EventIds.BarBeersRetrieved.ToEventId(), "Retrieval of bar-beers for {BarId} completed.", barId);
            
            return new OkObjectResult(barBeers);
        }

        [HttpGet]
        [Route("/bar/beer")]
        public ActionResult<List<BarBeer>> getAllBarsWithBeers()
        {
            logger.LogInformation(EventIds.BarBeersRetrievalStarted.ToEventId(), "Retrieval of bar-beers started.");

            List<BarBeer> allBarsWithBeers = barService.GetAllBarsWithBeers();
            
            logger.LogInformation(EventIds.BarBeersRetrieved.ToEventId(), "Retrieval of bar-beers completed.");

            return new OkObjectResult(allBarsWithBeers);
        }

        [HttpPost]
        [Route("/bar")]
        public IActionResult AddBar(BarPayload barPayload)
        {
            logger.LogInformation(EventIds.AddBarStarted.ToEventId(), "Add new bar started.");

            VMBar vmBar = new VMBar() { BarName = barPayload.BarName};
            ValidationResult validationResult = barService.ValidateBar(vmBar);

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
            barService.AddBar(barPayload);

            logger.LogInformation(EventIds.AddBarCompleted.ToEventId(), "Add new bar completed.");
            
            return new OkResult();
        }

        [HttpPut]
        [Route("/bar/{barId}")]
        public IActionResult UpdateBar(int barId, BarPayload barPayload)
        {
            logger.LogInformation(EventIds.UpdateBarStarted.ToEventId(), "Update bar {BarId} started.", barId);

            VMBar vmBar = new VMBar() { Id = barId, BarName = barPayload.BarName };
            ValidationResult validationResult = barService.ValidateBar(vmBar);

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
            barService.UpdateBar(barId, barPayload);

            logger.LogInformation(EventIds.UpdateBarCompleted.ToEventId(), "Update bar {BarId} completed.", barId);

            return new OkResult();
        }

        [HttpPost]
        [Route("/bar/beer")]
        public IActionResult AddBarBeer(BarBeerPayload barBeerPayload)
        {
            logger.LogInformation(EventIds.MapBarBeerStarted.ToEventId(), "Map bar {BarId} beer {BeerId} started.", barBeerPayload.BarId, barBeerPayload.BeerId);

            VMBarBeer vmBarBeer = new VMBarBeer() { BarId = barBeerPayload.BarId, BeerId = barBeerPayload.BeerId};
            ValidationResult validationResult = barService.ValidateBarBeer(vmBarBeer);

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
            
            barService.AddBarBeer(barBeerPayload);
            
            logger.LogInformation(EventIds.MapBarBeerCompleted.ToEventId(), "Map bar {BarId} beer {BeerId} completed.", barBeerPayload.BarId, barBeerPayload.BeerId);

            return new OkResult();
        }
    }
}
