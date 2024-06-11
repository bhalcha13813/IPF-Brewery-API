using FluentValidation.Results;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.Services
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

        public BeerResponseModel? getBeer(int beerId)
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

        public List<BeerResponseModel> getBeers()
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

        public List<BeerResponseModel> getBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
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

        public ValidationResult validateBeer(VMBeer vmBeer)
        {
            return _beerValidator.Validate(vmBeer);
        }

        public int addBeer(BeerPayload beerPayload)
        {
            Beer beer = new Beer()
            {
                BeerName = beerPayload.BeerName,
                PercentageAlcoholByVolume = beerPayload.PercentageAlcoholByVolume,
                BeerTypeId = beerPayload.BeerTypeId
            };
            return beerRepository.addBeer(beer);
        }

        public int updateBeer(int beerId, BeerPayload beerPayload)
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
