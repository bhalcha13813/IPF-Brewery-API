using FluentValidation.Results;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryValidator breweryValidator;
        private readonly IBreweryBeerValidator breweryBeerValidator;
        private readonly IBreweryRepository breweryRepository;
        private readonly IBeerRepository beerRepository;

        public BreweryService(IBreweryValidator breweryValidator,
                              IBreweryBeerValidator breweryBeerValidator,
                              IBreweryRepository breweryRepository, 
                              IBeerRepository beerRepository)
        {
            this.breweryValidator = breweryValidator;
            this.breweryBeerValidator = breweryBeerValidator;
            this.breweryRepository = breweryRepository;
            this.beerRepository = beerRepository;
        }

        public ValidationResult validateBrewery(VMBrewery vmBrewery)
        {
            return breweryValidator.Validate(vmBrewery);
        }

        public List<BreweryResponseModel> getBreweries()
        {
            return breweryRepository.getBreweries().Select(b => new BreweryResponseModel()
            {
                Id = b.Id,
                BreweryName = b.BreweryName,
                Address = b.Address
            }).ToList();
        }

        public BreweryResponseModel? getBrewery(int breweryId)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.getBrewery(breweryId);
            return brewery == null ? null : new BreweryResponseModel()
            {
                Id = brewery.Id,
                BreweryName = brewery.BreweryName,
                Address = brewery.Address
            };
        }

        public BreweryBeer? getBreweryBeers(int breweryId)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.getBreweryBeers(breweryId);
            return brewery == null ? null : new BreweryBeer()
                                            {
                                                Brewery = new BreweryResponseModel()
                                                {
                                                    Id = brewery.Id,
                                                    BreweryName = brewery.BreweryName,
                                                    Address = brewery.Address
                                                },
                                                Beers = brewery.Beer.Select(be => new BeerResponseModel()
                                                {
                                                    Id = be.Id,
                                                    BeerName = be.BeerName,
                                                    PercentageAlcoholByVolume = be.PercentageAlcoholByVolume,
                                                    BeerType = be.BeerType.BeerTypeName
                                                }).ToList()
                                            };
        }

        public List<BreweryBeer> getAllBreweriesWithBeers()
        {
            return breweryRepository.getAllBreweriesWithBeers().Select(b => new BreweryBeer()
            {
                Brewery = new BreweryResponseModel()
                {
                    Id = b.Id,
                    BreweryName = b.BreweryName,
                    Address = b.Address
                },
                Beers = b.Beer.Select(be => new BeerResponseModel()
                {
                    Id = be.Id,
                    BeerName = be.BeerName,
                    PercentageAlcoholByVolume = be.PercentageAlcoholByVolume,
                    BeerType = be.BeerType.BeerTypeName
                }).ToList()
            }).ToList();
        }

        public int addBrewery(BreweryPayload breweryPayload)
        {
            Common.Models.DTO.Brewery brewery = new Common.Models.DTO.Brewery()
            {
                BreweryName = breweryPayload.BreweryName,
                Address = breweryPayload.Address
            };
            return breweryRepository.addBrewery(brewery);
        }

        public int updateBrewery(int breweryId, BreweryPayload breweryPayload)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.getBrewery(breweryId);

            int updatedBreweries = 0;

            if (brewery != null)
            {
                brewery.BreweryName = breweryPayload.BreweryName;
                brewery.Address = breweryPayload.Address;

                updatedBreweries = breweryRepository.updateBrewery(brewery);
            }

            return updatedBreweries;
        }

        public ValidationResult validateBreweryBeer(VMBreweryBeer vmBreweryBeer)
        {
            return breweryBeerValidator.Validate(vmBreweryBeer);
        }

        public int addBreweryBeer(BreweryBeerPayload breweryBeerPayload)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.getBrewery(breweryBeerPayload.BreweryId);
            Beer? beer = beerRepository.getBeer(breweryBeerPayload.BeerId);

            int updatedBreweryBeer = 0;

            if (brewery != null && beer != null)
            {
                brewery.Beer.Add(beer);
                updatedBreweryBeer = breweryRepository.updateBrewery(brewery);
            }
            return updatedBreweryBeer;
        }
    }
}
