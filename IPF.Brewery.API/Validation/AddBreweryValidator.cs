using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.Common.Models.Request;

namespace IPF.Brewery.API.Validation
{
    public interface IAddBreweryValidator
    {
        ValidationResult Validate(BreweryPayload breweryPayload);

    }

    public class AddBreweryValidator : AbstractValidator<BreweryPayload>, IAddBreweryValidator
    {
        private readonly IBreweryRepository breweryRepository;

        private Common.Models.DTO.Brewery brewery;

        ValidationResult IAddBreweryValidator.Validate(BreweryPayload breweryPayload)
        {
            return Validate(breweryPayload);
        }

        public AddBreweryValidator(IBreweryRepository breweryRepository)
        {
            this.breweryRepository = breweryRepository;

            RuleFor(b => b.BreweryName).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BreweryName cannot be empty.");

            RuleFor(b => b.BreweryName)
                .Must(b => BeUniqueBreweryName(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("Brewery name already exists.");
        }

        private Common.Models.DTO.Brewery getBrewery(string breweryName)
        {
            if (brewery == null)
            {
                brewery = breweryRepository.getBrewery(breweryName);
            }

            return brewery;
        }

        private bool BeUniqueBreweryName(string breweryName)
        { 
            brewery = getBrewery(breweryName);
            return brewery == null;
        }
    }
}
