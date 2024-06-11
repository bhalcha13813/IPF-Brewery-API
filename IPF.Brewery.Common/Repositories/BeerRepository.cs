using Microsoft.EntityFrameworkCore;
using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BreweryContext breweryContext;

        public BeerRepository(BreweryContext breweryContext )
        {
            this.breweryContext = breweryContext;
        }

        public Beer? getBeer(int beerId)
        {
            return breweryContext.Beer.Include(b => b.BeerType).FirstOrDefault(b => b.Id == beerId);
        }

        public Beer? getBeer(string beerName)
        {
            return breweryContext.Beer.FirstOrDefault(b => b.BeerName== beerName);
        }

        public IQueryable<Beer> getBeers()
        {
            return breweryContext.Beer.Include(b => b.BeerType);
        }

        public IQueryable<Beer> getBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            return breweryContext.Beer
                .Include(b => b.BeerType)
                .Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume &&
                            b.PercentageAlcoholByVolume < ltAlcoholByVolume);
        }

        public int addBeer(Beer beer)
        {
            breweryContext.Add(beer);
            return breweryContext.SaveChanges();
        }

        public int updateBeer(Beer beer)
        {
            breweryContext.Update(beer);
            return breweryContext.SaveChanges();
        }
    }
}