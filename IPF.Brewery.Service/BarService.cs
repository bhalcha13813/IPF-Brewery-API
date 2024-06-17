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
        public BarService(IBarValidator barValidator, IBarBeerValidator barBeerValidator, IBarRepository barRepository, IBeerRepository beerRepository)
        {
            this.barValidator = barValidator;
            this.barBeerValidator = barBeerValidator;
            this.barRepository = barRepository;
            this.beerRepository = beerRepository;
        }

        /// <summary>
        /// This method is used to get all bars from database.
        /// </summary>
        /// <returns>List of bars</returns>
        public List<BarResponseModel> GetBars()
        {
           return barRepository.getBars()
                               .Select(b => new BarResponseModel()
                               {
                                   Id = b.Id,
                                   BarName = b.BarName,
                                   Address = b.Address
                               }).ToList();
        }

        /// <summary>
        /// This method is used to get specific bar that matches with provided barId.
        /// </summary>
        /// <param name="barId"></param>
        /// <returns>bar</returns>
        public BarResponseModel? GetBar(int barId)
        {
            Bar? bar = barRepository.getBar(barId);
            return bar == null ? null : new BarResponseModel()
            {
                Id = bar.Id,
                BarName = bar.BarName,
                Address = bar.Address
            };
        }

        /// <summary>
        /// This method is used to get bar with its beers that matches with provided barId.
        /// </summary>
        /// <param name="barId"></param>
        /// <returns>BarBeers</returns>
        public BarBeer? GetBarBeers(int barId)
        {
            Bar? bar = barRepository.getBarBeers(barId);
            return bar == null ? null : new BarBeer()
                                        {
                                                Bar = new BarResponseModel()
                                                {
                                                    Id = bar.Id,
                                                    BarName = bar.BarName,
                                                    Address = bar.Address,
                                                },
                                                Beers = bar.Beer.Select(be => new BeerResponseModel()
                                                {
                                                    Id = be.Id,
                                                    BeerName = be.BeerName,
                                                    PercentageAlcoholByVolume = be.PercentageAlcoholByVolume,
                                                    BeerType = be.BeerType.BeerTypeName
                                                }).ToList()
                                        };
        }

        /// <summary>
        /// This method is used to get all bars with respective beers.
        /// </summary>
        /// <returns>BarBeers</returns>
        public List<BarBeer> GetAllBarsWithBeers()
        {
            return barRepository.getAllBarsWithBeers()
                                .Select(b => new BarBeer()
                                {
                                    Bar = new BarResponseModel()
                                    {
                                        Id = b.Id,
                                        BarName = b.BarName,
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
            Bar bar = new Bar()
            {
                BarName = barPayload.BarName,
                Address = barPayload.Address
            };
            return barRepository.addBar(bar);
        }

        /// <summary>
        /// This method is used to update existing bar in database.
        /// </summary>
        /// <param name="barId"></param>
        /// <param name="barPayload"></param>
        /// <returns>number of bars updated</returns>
        public int UpdateBar(int barId, BarPayload barPayload)
        {
            Bar? bar = barRepository.getBar(barId);

            int updatedBars = 0;

            if (bar != null)
            {
                bar.BarName = barPayload.BarName;
                bar.Address = barPayload.Address;

                updatedBars = barRepository.updateBar(bar);
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
            Bar? bar = barRepository.getBar(barBeerPayload.BarId);
            Beer? beer = beerRepository.getBeer(barBeerPayload.BeerId);

            int updatedBarBeer = 0;

            if (bar != null && beer != null)
            {
                bar.Beer.Add(beer);
                updatedBarBeer = barRepository.updateBar(bar);
            }
            return updatedBarBeer;
        }
    }
}
