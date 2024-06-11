using FluentValidation;
using FluentValidation.Results;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;
using System.Net;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.API.Validation
{
    public interface IBreweryValidator
    {
        ValidationResult Validate(VMBrewery vmBrewery);

    }

    public class BreweryValidator : AbstractValidator<VMBrewery>, IBreweryValidator
    {
        private readonly IBreweryRepository breweryRepository;

        private Common.Models.DTO.Brewery brewery;

        ValidationResult IBreweryValidator.Validate(VMBrewery vmBrewery)
        {
            return Validate(vmBrewery);
        }

        public BreweryValidator(IBreweryRepository breweryRepository)
        {
            this.breweryRepository = breweryRepository;

            RuleFor(b => b.BreweryName).NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("BreweryName cannot be empty.");

            RuleFor(b => b)
                .Must(b => BeUniqueBreweryName(b))
                .WithErrorCode(HttpStatusCode.Conflict.ToString())
                .WithMessage("Brewery name already exists.");
        }

        private Common.Models.DTO.Brewery getBrewery(VMBrewery vmBrewery)
        {
            if (brewery == null)
            {
                brewery = breweryRepository.getBrewery(vmBrewery.BreweryName);

                if (brewery != null && vmBrewery.Id != null)
                {
                    if (vmBrewery.Id.Value == brewery.Id)
                    {
                        brewery = null;
                    }
                }
            }

            return brewery;
        }

        private bool BeUniqueBreweryName(VMBrewery vmBrewery)
        { 
            brewery = getBrewery(vmBrewery);
            return brewery == null;
        }
    }
}
