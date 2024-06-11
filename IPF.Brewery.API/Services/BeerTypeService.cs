using FluentValidation.Results;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.Services
{
    public class BeerTypeService : IBeerTypeService
    {
        private readonly IBeerTypeValidator addBeerTypeValidator;
        private readonly IBeerTypeRepository beerTypeRepository;
        
        public BeerTypeService(IBeerTypeValidator addBeerTypeValidator, IBeerTypeRepository beerTypeRepository)
        {
            this.addBeerTypeValidator = addBeerTypeValidator;
            this.beerTypeRepository = beerTypeRepository;
        }

        public List<BeerTypeResponseModel> getBeerTypes()
        {
           return beerTypeRepository.getBeerTypes()
                  .Select(b => new BeerTypeResponseModel()
                                        {
                                            Id = b.Id,
                                            BeerType = b.BeerTypeName
                                        }).ToList();
        }

        public ValidationResult validateBeerType(VMBeerType vmBeerType)
        {
            return addBeerTypeValidator.Validate(vmBeerType);
        }

        public int addBeerType(BeerTypePayload beerTypePayload)
        {
            BeerType beerType = new BeerType()
            {
                BeerTypeName = beerTypePayload.BeerType,
            };
            return beerTypeRepository.addBeerType(beerType);
        }

        public int updateBeerType(int beerTypeId, BeerTypePayload beerTypePayload)
        {
            BeerType? beerType = beerTypeRepository.getBeerType(beerTypeId);

            int updatedBeerTypes = 0;

            if (beerType != null)
            {
                beerType.BeerTypeName = beerTypePayload.BeerType;

                updatedBeerTypes = beerTypeRepository.updateBeerType(beerType);
            }

            return updatedBeerTypes;
        }
    }
}
