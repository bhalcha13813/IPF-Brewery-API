using FluentValidation.Results;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.Service
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

        /// <summary>
        /// This method is used to get all beer types from database.
        /// </summary>
        /// <returns>List of beer types</returns>
        public List<BeerTypeResponseModel> GetBeerTypes()
        {
           return beerTypeRepository.getBeerTypes()
                  .Select(b => new BeerTypeResponseModel()
                                        {
                                            Id = b.Id,
                                            BeerType = b.BeerTypeName
                                        }).ToList();
        }

        /// <summary>
        /// This method is used to validate beer type provided before add/update.
        /// </summary>
        /// <returns>validation result</returns>
        public ValidationResult ValidateBeerType(VMBeerType vmBeerType)
        {
            return addBeerTypeValidator.Validate(vmBeerType);
        }

        /// <summary>
        /// This method is used to add new beer type in database.
        /// </summary>
        /// <param name="beerTypePayload"></param>
        /// <returns>number of beer types added</returns>
        public int AddBeerType(BeerTypePayload beerTypePayload)
        {
            BeerType beerType = new BeerType()
            {
                BeerTypeName = beerTypePayload.BeerType,
            };
            return beerTypeRepository.addBeerType(beerType);
        }

        /// <summary>
        /// This method is used to update existing beer type in database.
        /// </summary>
        /// <param name="beerTypeId"></param>
        /// <param name="beerTypePayload"></param>
        /// <returns>number of beer types updated</returns>
        public int UpdateBeerType(int beerTypeId, BeerTypePayload beerTypePayload)
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
