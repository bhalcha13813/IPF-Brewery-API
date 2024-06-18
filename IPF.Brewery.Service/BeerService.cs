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
    public class BeerService : IBeerService
    {
        private readonly IBeerValidator _beerValidator;
        private readonly IBeerRepository beerRepository;
        private readonly IMapper mapper;

        public BeerService(IBeerValidator beerValidator, 
                            IBeerRepository beerRepository, 
                            IMapper mapper)
        {
            this._beerValidator = beerValidator;
            this.beerRepository = beerRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method is used to get specific beer that matches with provided beerId.
        /// </summary>
        /// <param name="beerId"></param>
        /// <returns>Beer</returns>
        public BeerResponseModel? GetBeer(int beerId)
        {
           Beer? beer =  beerRepository.GetBeer(beerId);
           return mapper.Map<BeerResponseModel>(beer);
        }

        /// <summary>
        /// This method is used to get all beers from database.
        /// </summary>
        /// <returns>List of beers</returns>
        public List<BeerResponseModel> GetBeers()
        {
            return beerRepository.GetBeers()
                                 .Select(b => mapper.Map<BeerResponseModel>(b))
                                 .ToList();
        }

        /// <summary>
        /// This method is used to get all beers within the alcohol percentage range provided.
        /// </summary>
        /// <param name="gtAlcoholByVolume"></param>
        /// <param name="ltAlcoholByVolume"></param>
        /// <returns>List of beers</returns>
        public List<BeerResponseModel> GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        { 
            return beerRepository.GetBeers(gtAlcoholByVolume, ltAlcoholByVolume)
                                 .Select(b => mapper.Map<BeerResponseModel>(b))
                                 .ToList();
        }

        /// <summary>
        /// This method is used to validate beer details provided before add/update.
        /// </summary>
        /// <returns>validation result</returns>
        public ValidationResult ValidateBeer(VMBeer vmBeer)
        {
            return _beerValidator.Validate(vmBeer);
        }

        /// <summary>
        /// This method is used to add new beer in database.
        /// </summary>
        /// <param name="beerPayload"></param>
        /// <returns>number of beers added</returns>
        public int AddBeer(BeerPayload beerPayload)
        {
            Beer? beer = mapper.Map<Beer>(beerPayload);
            return beerRepository.AddBeer(beer);
        }

        /// <summary>
        /// This method is used to update existing beer in database.
        /// </summary>
        /// <param name="beerId"></param>
        /// <param name="beerPayload"></param>
        /// <returns>number of beers uopdated</returns>
        public int UpdateBeer(int beerId, BeerPayload beerPayload)
        {
            Beer? beer = beerRepository.GetBeer(beerId);
            
            int updatedBeers = 0;

            if (beer != null)
            {
                beer.BeerName = beerPayload.BeerName;
                beer.PercentageAlcoholByVolume = beerPayload.PercentageAlcoholByVolume;
                beer.BeerTypeId = beerPayload.BeerTypeId;

                updatedBeers = beerRepository.UpdateBeer(beer);
            }

            return updatedBeers;
        }
    }
}
