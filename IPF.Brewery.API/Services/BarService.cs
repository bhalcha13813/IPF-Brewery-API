using FluentValidation.Results;
using IPF.Brewery.API.Validation;
using IPF.Brewery.API.Validation.Models;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;
using IPF.Brewery.Common.Repositories;

namespace IPF.Brewery.API.Services
{
    public class BarService : IBarService
    {
        private readonly IBarValidator _barValidator;
        private readonly IBarRepository barRepository;
        private readonly IBeerRepository beerRepository;
        public BarService(IBarValidator barValidator, IBarRepository barRepository, IBeerRepository beerRepository)
        {
            this._barValidator = barValidator;
            this.barRepository = barRepository;
            this.beerRepository = beerRepository;
        }

        public ValidationResult validateBar(VMBar vmBar)
        {
            return _barValidator.Validate(vmBar);
        }

        public List<BarResponseModel> getBars()
        {
           return barRepository.getBars()
                               .Select(b => new BarResponseModel()
                               {
                                   Id = b.Id,
                                   BarName = b.BarName,
                                   Address = b.Address
                               }).ToList();
        }

        public BarResponseModel? getBar(int barId)
        {
            Bar? bar = barRepository.getBar(barId);
            return bar == null ? null : new BarResponseModel()
            {
                Id = bar.Id,
                BarName = bar.BarName,
                Address = bar.Address
            };
        }

        public List<BarBeer> getBarBeers(int barId)
        {
            return barRepository.getBarBeers(barId)
                .Select(b => new BarBeer()
                {
                    Bar = new BarResponseModel()
                    {
                        Id = b.Id,
                        BarName = b.BarName,
                        Address = b.Address,
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

        public List<BarBeer> getAllBarsWithBeers()
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

        public int addBar(BarPayload barPayload)
        {
            Bar bar = new Bar()
            {
                BarName = barPayload.BarName,
                Address = barPayload.Address
            };
            return barRepository.addBar(bar);
        }

        public int updateBar(int barId, BarPayload barPayload)
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

        public int addBarBeer(BarBeerPayload barBeerPayload)
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
