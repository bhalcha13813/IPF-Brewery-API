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
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryValidator breweryValidator;
        private readonly IBreweryBeerValidator breweryBeerValidator;
        private readonly IBreweryRepository breweryRepository;
        private readonly IBeerRepository beerRepository;
        private readonly IMapper mapper;

        public BreweryService(IBreweryValidator breweryValidator,
                              IBreweryBeerValidator breweryBeerValidator,
                              IBreweryRepository breweryRepository, 
                              IBeerRepository beerRepository,
                              IMapper mapper)
        {
            this.breweryValidator = breweryValidator;
            this.breweryBeerValidator = breweryBeerValidator;
            this.breweryRepository = breweryRepository;
            this.beerRepository = beerRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method is used to get all breweries from database.
        /// </summary>
        /// <returns>List of breweries</returns>
        public List<BreweryResponseModel> GetBreweries()
        {
            return breweryRepository.GetBreweries().Select(b => mapper.Map<BreweryResponseModel>(b)).ToList();
        }

        /// <summary>
        /// This method is used to get specific brewery that matches with provided BreweryId.
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns>brewery</returns>
        public BreweryResponseModel? GetBrewery(int breweryId)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.GetBrewery(breweryId);
            return mapper.Map<BreweryResponseModel>(brewery);
        }

        /// <summary>
        /// This method is used to get brewery with its beers that matches with provided BreweryId.
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns>BreweryBeers</returns>
        public BreweryBeer? GetBreweryBeers(int breweryId)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.GetBreweryBeers(breweryId);
            BreweryBeer? breweryBeers = mapper.Map<BreweryBeer>(brewery);
            return breweryBeers;
        }

        /// <summary>
        /// This method is used to get all breweries with respective beers.
        /// </summary>
        /// <returns>BreweryBeers</returns>
        public List<BreweryBeer> GetAllBreweriesWithBeers()
        {
            return breweryRepository.GetAllBreweriesWithBeers().Select(b => mapper.Map<BreweryBeer>(b)).ToList();
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
            Common.Models.DTO.Brewery brewery = mapper.Map<Common.Models.DTO.Brewery>(breweryPayload);
            return breweryRepository.AddBrewery(brewery);
        }

        /// <summary>
        /// This method is used to update existing brewery in database.
        /// </summary>
        /// <param name="breweryId"></param>
        /// <param name="breweryPayload"></param>
        /// <returns>number of breweries updated</returns>
        public int UpdateBrewery(int breweryId, BreweryPayload breweryPayload)
        {
            Common.Models.DTO.Brewery? brewery = breweryRepository.GetBrewery(breweryId);

            int updatedBreweries = 0;

            if (brewery != null)
            {
                brewery.BreweryName = breweryPayload.BreweryName;
                brewery.Address = breweryPayload.Address;

                updatedBreweries = breweryRepository.UpdateBrewery(brewery);
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
            Common.Models.DTO.Brewery? brewery = breweryRepository.GetBrewery(breweryBeerPayload.BreweryId);
            Beer? beer = beerRepository.GetBeer(breweryBeerPayload.BeerId);

            int updatedBreweryBeer = 0;

            if (brewery != null && beer != null)
            {
                brewery.Beer.Add(beer);
                updatedBreweryBeer = breweryRepository.UpdateBrewery(brewery);
            }
            return updatedBreweryBeer;
        }
    }
}
