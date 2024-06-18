using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.API.Validation
{
    public interface IBarBeerValidator
    {
        ValidationResult Validate(VMBarBeer vmBarBeer);
    }

    public class BarBeerValidator : AbstractValidator<VMBarBeer>, IBarBeerValidator
    {
        private readonly IBarRepository barRepository;
        private readonly IBeerRepository beerRepository;

        private Bar bar;
        private Beer beer;

        ValidationResult IBarBeerValidator.Validate(VMBarBeer vmBarBeer)
        {
            return Validate(vmBarBeer);
        }

        public BarBeerValidator(IBarRepository barRepository, IBeerRepository beerRepository)
        {
            this.barRepository = barRepository;
            this.beerRepository = beerRepository;

            RuleFor(b => b.BarId)
                .Must(b => BeExistingBar(b))
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("Bar does not exist, Please add Bar first.");

            RuleFor(b => b.BeerId)
                .Must(b => BeExistingBeer(b))
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("Beer does not exist, Please add Beer first.");

            RuleFor(b => b)
                .Must(b => NotBeExistingBarBeer(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("This Bar-Beer record already exists.")
                .OverridePropertyName("BarBeer");
        }

        private Bar getBar(int barId)
        {
            if (bar == null)
            {
                bar = barRepository.GetBar(barId);
            }

            return bar;
        }

        private Beer getBeer(int beerId)
        {
            if (beer == null)
            {
                beer = beerRepository.GetBeer(beerId);
            }

            return beer;
        }


        private bool BeExistingBar(int barId)
        { 
            bar = getBar(barId);
            return bar != null;
        }

        private bool BeExistingBeer(int beerId)
        {
            beer = getBeer(beerId);
            return beer != null;
        }

        private bool NotBeExistingBarBeer(VMBarBeer vmBarBeer)
        {
            Bar? barBeers = barRepository.GetBarBeers(vmBarBeer.BarId);
            if (barBeers != null)
            {
               int beerCount = barBeers.Beer.Count(b => b.Id == vmBarBeer.BeerId);
               if (beerCount == 1)
                   return false;
            }

            return true;
        }
    }
}
