using AutoMapper;
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
        private readonly IMapper mapper;
        
        public BeerTypeService(IBeerTypeValidator addBeerTypeValidator, 
                                IBeerTypeRepository beerTypeRepository, 
                                IMapper mapper)
        {
            this.addBeerTypeValidator = addBeerTypeValidator;
            this.beerTypeRepository = beerTypeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method is used to get all beer types from database.
        /// </summary>
        /// <returns>List of beer types</returns>
        public List<BeerTypeResponseModel> GetBeerTypes()
        {
           return beerTypeRepository.GetBeerTypes()
                                    .Select(b => mapper.Map<BeerTypeResponseModel>(b))
                                    .ToList();
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
            BeerType beerType = mapper.Map<BeerType>(beerTypePayload);
            return beerTypeRepository.AddBeerType(beerType);
        }

        /// <summary>
        /// This method is used to update existing beer type in database.
        /// </summary>
        /// <param name="beerTypeId"></param>
        /// <param name="beerTypePayload"></param>
        /// <returns>number of beer types updated</returns>
        public int UpdateBeerType(int beerTypeId, BeerTypePayload beerTypePayload)
        {
            BeerType? beerType = beerTypeRepository.GetBeerType(beerTypeId);

            int updatedBeerTypes = 0;

            if (beerType != null)
            {
                beerType.BeerTypeName = beerTypePayload.BeerType;

                updatedBeerTypes = beerTypeRepository.UpdateBeerType(beerType);
            }

            return updatedBeerTypes;
        }
    }
}
