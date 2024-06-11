﻿using IPF.Brewery.Common.Models.DTO;
using System.Linq;

namespace IPF.Brewery.Common.Repositories
{
    public interface IBeerTypeRepository
    {
        IQueryable<BeerType> getBeerTypes();
        BeerType? getBeerType(int beerTypeId);
        BeerType? getBeerType(string beerType);
        int addBeerType(BeerType beerType);
        int updateBeerType(BeerType beerType);
    }
}