using FluentValidation.Results;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.Service
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

        /// <summary>
        /// This method is used to get all breweries from database.
        /// </summary>
        /// <returns>List of breweries</returns>
        public List<BreweryResponseModel> GetBreweries()
        {
            return breweryRepository.getBreweries().Select(b => new BreweryResponseModel()
            {
                Id = b.Id,
                BreweryName = b.BreweryName,
                Address = b.Address
            }).ToList();
        }

        /// <summary>
        /// This method is used to get specific brewery that matches with provided BreweryId.
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns>brewery</returns>
        public BreweryResponseModel? GetBrewery(int breweryId)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.getBrewery(breweryId);
            return brewery == null ? null : new BreweryResponseModel()
            {
                Id = brewery.Id,
                BreweryName = brewery.BreweryName,
                Address = brewery.Address
            };
        }

        /// <summary>
        /// This method is used to get brewery with its beers that matches with provided BreweryId.
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns>BreweryBeers</returns>
        public BreweryBeer? GetBreweryBeers(int breweryId)
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

        /// <summary>
        /// This method is used to get all breweries with respective beers.
        /// </summary>
        /// <returns>BreweryBeers</returns>
        public List<BreweryBeer> GetAllBreweriesWithBeers()
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

        /// <summary>
        /// This method is used to validate brewery details provided before add/update.
        /// </summary>
        /// <returns>validation result</returns>
        public ValidationResult ValidateBrewery(VMBrewery vmBrewery)
        {
            return breweryValidator.Validate(vmBrewery);
        }

        /// <summary>
        /// This method is used to add new brewery in database.
        /// </summary>
        /// <param name="breweryPayload"></param>
        /// <returns>number of breweries added</returns>
        public int AddBrewery(BreweryPayload breweryPayload)
        {
            Common.Models.DTO.Brewery brewery = new Common.Models.DTO.Brewery()
            {
                BreweryName = breweryPayload.BreweryName,
                Address = breweryPayload.Address
            };
            return breweryRepository.addBrewery(brewery);
        }

        /// <summary>
        /// This method is used to update existing brewery in database.
        /// </summary>
        /// <param name="breweryId"></param>
        /// <param name="breweryPayload"></param>
        /// <returns>number of breweries updated</returns>
        public int UpdateBrewery(int breweryId, BreweryPayload breweryPayload)
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

        /// <summary>
        /// This method is used to validate BreweryBeer before mapping.
        /// </summary>
        /// <param name="vmBreweryBeer"></param>
        /// <returns>validation result</returns>
        public ValidationResult ValidateBreweryBeer(VMBreweryBeer vmBreweryBeer)
        {
            return breweryBeerValidator.Validate(vmBreweryBeer);
        }

        /// <summary>
        /// This method is used to map Brewery & Beer.
        /// </summary>
        /// <param name="breweryBeerPayload"></param>
        /// <returns>number of brewery beers mapped</returns>
        public int AddBreweryBeer(BreweryBeerPayload breweryBeerPayload)
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
