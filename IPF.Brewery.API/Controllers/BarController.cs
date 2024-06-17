using FluentValidation.Results;
using IPF.Brewery.API.Extension;
using IPF.Brewery.API.Services;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace IPF.Brewery.API.Controllers
{
    public class BarController : BaseController
    {
        private readonly IBarService barService; 
        public BarController(IHttpContextAccessor contextAccessor,
                             IBarService barService) : base(contextAccessor)
        {
            this.barService = barService;
        }


        [HttpGet]
        [Route("/bar")]
        public IActionResult GetBars()
        {
           List<BarResponseModel> bars = barService.GetBars();
           return new OkObjectResult(bars);
        }

        [HttpGet]
        [Route("/bar/{barId}")]
        public IActionResult GetBar(int barId)
        {
            BarResponseModel? bar = barService.GetBar(barId);
            return new OkObjectResult(bar);
        }

        [HttpGet]
        [Route("/bar/{barId}/beer")]
        public IActionResult GetBarBeers(int barId)
        {
            BarBeer? barBeers = barService.GetBarBeers(barId);
            return new OkObjectResult(barBeers);
        }

        [HttpGet]
        [Route("/bar/beer")]
        public IActionResult getAllBarsWithBeers()
        {
            List<BarBeer> allBarsWithBeers = barService.GetAllBarsWithBeers();
            return new OkObjectResult(allBarsWithBeers);
        }

        [HttpPost]
        [Route("/bar")]
        public IActionResult AddBar(BarPayload barPayload)
        {
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
            return new OkResult();
        }

        [HttpPut]
        [Route("/bar/{barId}")]
        public IActionResult UpdateBar(int barId, BarPayload barPayload)
        {
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
            return new OkResult();
        }

        [HttpPost]
        [Route("/bar/beer")]
        public IActionResult AddBarBeer(BarBeerPayload barBeerPayload)
        {
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
            return new OkResult();
        }
    }
}
