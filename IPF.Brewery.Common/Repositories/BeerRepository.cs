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

        public Beer? GetBeer(int beerId)
        {
            return breweryContext.Beer.Include(b => b.BeerType).FirstOrDefault(b => b.Id == beerId);
        }

        public Beer? GetBeer(string beerName)
        {
            return breweryContext.Beer.FirstOrDefault(b => b.BeerName== beerName);
        }

        public IQueryable<Beer> GetBeers()
        {
            return breweryContext.Beer.Include(b => b.BeerType);
        }

        public IQueryable<Beer> GetBeers(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            return breweryContext.Beer
                .Include(b => b.BeerType)
                .Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume &&
                            b.PercentageAlcoholByVolume < ltAlcoholByVolume);
        }

        public int AddBeer(Beer beer)
        {
            breweryContext.Add(beer);
            return breweryContext.SaveChanges();
        }

        public int UpdateBeer(Beer beer)
        {
            breweryContext.Update(beer);
            return breweryContext.SaveChanges();
        }
    }
}