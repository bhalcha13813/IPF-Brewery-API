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
    public class BarService : IBarService
    {
        private readonly IBarValidator barValidator;
        private readonly IBarBeerValidator barBeerValidator;
        private readonly IBarRepository barRepository;
        private readonly IBeerRepository beerRepository;
        private readonly IMapper mapper;

        public BarService(IBarValidator barValidator, 
                            IBarBeerValidator barBeerValidator, 
                            IBarRepository barRepository, 
                            IBeerRepository beerRepository,
                            IMapper mapper)
        {
            this.barValidator = barValidator;
            this.barBeerValidator = barBeerValidator;
            this.barRepository = barRepository;
            this.beerRepository = beerRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method is used to get all bars from database.
        /// </summary>
        /// <returns>List of bars</returns>
        public List<BarResponseModel> GetBars()
        {
           return barRepository.GetBars()
                               .Select(b => mapper.Map<BarResponseModel>(b))
                               .ToList();
        }

        /// <summary>
        /// This method is used to get specific bar that matches with provided barId.
        /// </summary>
        /// <param name="barId"></param>
        /// <returns>bar</returns>
        public BarResponseModel? GetBar(int barId)
        {
            Bar? bar = barRepository.GetBar(barId);
            return mapper.Map<BarResponseModel>(bar);
        }

        /// <summary>
        /// This method is used to get bar with its beers that matches with provided barId.
        /// </summary>
        /// <param name="barId"></param>
        /// <returns>BarBeers</returns>
        public BarBeer? GetBarBeers(int barId)
        {
            Bar? bar = barRepository.GetBarBeers(barId);
            BarBeer? barBeers = mapper.Map<BarBeer>(bar);
            return barBeers;
        }

        /// <summary>
        /// This method is used to get all bars with respective beers.
        /// </summary>
        /// <returns>BarBeers</returns>
        public List<BarBeer> GetAllBarsWithBeers()
        {
            return barRepository.GetAllBarsWithBeers().Select(b => mapper.Map<BarBeer>(b)).ToList();
        }

        /// <summary>
        /// This method is used to validate bar details provided before add/update.
        /// </summary>
        /// <returns>validation result</returns>
        public ValidationResult ValidateBar(VMBar vmBar)
        {
            return barValidator.Validate(vmBar);
        }

        /// <summary>
        /// This method is used to add new bar in database.
        /// </summary>
        /// <param name="barPayload"></param>
        /// <returns>number of bars added</returns>
        public int AddBar(BarPayload barPayload)
        {
            Bar? bar = mapper.Map<Bar>(barPayload);
            return barRepository.AddBar(bar);
        }

        /// <summary>
        /// This method is used to update existing bar in database.
        /// </summary>
        /// <param name="barId"></param>
        /// <param name="barPayload"></param>
        /// <returns>number of bars updated</returns>
        public int UpdateBar(int barId, BarPayload barPayload)
        {
            Bar? bar = barRepository.GetBar(barId);

            int updatedBars = 0;

            if (bar != null)
            {
                bar.BarName = barPayload.BarName;
                bar.Address = barPayload.Address;

                updatedBars = barRepository.UpdateBar(bar);
            }

            return updatedBars;
        }

        /// <summary>
        /// This method is used to validate BarBeer before mapping.
        /// </summary>
        /// <param name="vmBarBeer"></param>
        /// <returns>validation result</returns>
        public ValidationResult ValidateBarBeer(VMBarBeer vmBarBeer)
        {
            return barBeerValidator.Validate(vmBarBeer);
        }

        /// <summary>
        /// This method is used to map Bar & Beer.
        /// </summary>
        /// <param name="barBeerPayload"></param>
        /// <returns>number of bar beers mapped</returns>
        public int AddBarBeer(BarBeerPayload barBeerPayload)
        {
            Bar? bar = barRepository.GetBar(barBeerPayload.BarId);
            Beer? beer = beerRepository.GetBeer(barBeerPayload.BeerId);

            int updatedBarBeer = 0;

            if (bar != null && beer != null)
            {
                bar.Beer.Add(beer);
                updatedBarBeer = barRepository.UpdateBar(bar);
            }
            return updatedBarBeer;
        }
    }
}
