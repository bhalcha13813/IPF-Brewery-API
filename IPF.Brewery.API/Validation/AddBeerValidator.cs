using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;

namespace IPF.Brewery.API.Validation
{
    public interface IAddBeerValidator
    {
        ValidationResult Validate(BeerPayload beerPayload);

    }

    public class AddBeerValidator : AbstractValidator<BeerPayload>, IAddBeerValidator
    {
        private readonly IBeerRepository beerRepository;

        private Beer beer;

        ValidationResult IAddBeerValidator.Validate(BeerPayload beerPayload)
        {
            return Validate(beerPayload);
        }

        public AddBeerValidator(IBeerRepository beerRepository)
        {
            this.beerRepository = beerRepository;

            RuleFor(b => b.BeerName).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BeerName cannot be empty.");

            RuleFor(b => b.BeerName)
                .Must(b => BeUniqueBeerName(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("BeerName already exists.");
        }

        private Beer getBeer(string beerName)
        {
            if (beer == null)
            {
                beer = beerRepository.getBeer(beerName);
            }

            return beer;
        }

        private bool BeUniqueBeerName(string beerName)
        { 
            beer = getBeer(beerName);
            return beer == null;
        }
    }
}
