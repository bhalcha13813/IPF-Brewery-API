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
        
        public BeerService(IBeerValidator beerValidator, IBeerRepository beerRepository)
        {
            this._beerValidator = beerValidator;
            this.beerRepository = beerRepository;
        }

        /// <summary>
        /// This method is used to get specific beer that matches with provided beerId.
        /// </summary>
        /// <param name="beerId"></param>
        /// <returns>Beer</returns>
        public BeerResponseModel? GetBeer(int beerId)
        {
           Beer? beer =  beerRepository.getBeer(beerId);

           return beer == null ? null : new BeerResponseModel()
                                           {
                                               Id = beer.Id,
                                               BeerName = beer.BeerName,
                                               PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume,
                                               BeerType = beer.BeerType.BeerTypeName
                                           };
        }

        /// <summary>
        /// This method is used to get all beers from database.
        /// </summary>
        /// <returns>List of beers</returns>
        public List<BeerResponseModel> GetBeers()
        {
            return beerRepository.getBeers()
                .Select(b => new BeerResponseModel()
                {
                    Id = b.Id,
                    BeerName = b.BeerName,
                    PercentageAlcoholByVolume = b.PercentageAlcoholByVolume,
                    BeerType = b.BeerType.BeerTypeName
                }).ToList();
        }

        /// <summary>
        /// This method is used to get all beers within the alcohol percentage range provided.
        /// </summary>
        /// <param name="gtAlcoholByVolume"></param>
        /// <param name="ltAlcoholByVolume"></param>
        /// <returns>List of beers</returns>
        public List<BeerResponseModel> GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        { 
            return beerRepository.getBeers(gtAlcoholByVolume, ltAlcoholByVolume)
                    .Select(b => new BeerResponseModel()
                    {
                       Id = b.Id,
                       BeerName = b.BeerName,
                       PercentageAlcoholByVolume = b.PercentageAlcoholByVolume,
                       BeerType = b.BeerType.BeerTypeName
                    }).ToList();
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
            Beer beer = new Beer()
            {
                BeerName = beerPayload.BeerName,
                PercentageAlcoholByVolume = beerPayload.PercentageAlcoholByVolume,
                BeerTypeId = beerPayload.BeerTypeId
            };
            return beerRepository.addBeer(beer);
        }

        /// <summary>
        /// This method is used to update existing beer in database.
        /// </summary>
        /// <param name="beerId"></param>
        /// <param name="beerPayload"></param>
        /// <returns>number of beers uopdated</returns>
        public int UpdateBeer(int beerId, BeerPayload beerPayload)
        {
            Beer? beer = beerRepository.getBeer(beerId);
            
            int updatedBeers = 0;

            if (beer != null)
            {
                beer.BeerName = beerPayload.BeerName;
                beer.PercentageAlcoholByVolume = beerPayload.PercentageAlcoholByVolume;
                beer.BeerTypeId = beerPayload.BeerTypeId;

                updatedBeers = beerRepository.updateBeer(beer);
            }

            return updatedBeers;
        }
    }
}
