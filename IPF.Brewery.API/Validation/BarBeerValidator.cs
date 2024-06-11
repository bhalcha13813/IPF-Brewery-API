using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Response;

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
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("Bar does not exist, Please add Bar first.");

            RuleFor(b => b.BeerId)
                .Must(b => BeExistingBeer(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("Beer does not exist, Please add Beer first.");
        }

        private Bar getBar(int barId)
        {
            if (bar == null)
            {
                bar = barRepository.getBar(barId);
            }

            return bar;
        }

        private Beer getBeer(int beerId)
        {
            if (beer == null)
            {
                beer = beerRepository.getBeer(beerId);
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
    }
}
