using Microsoft.EntityFrameworkCore;

namespace IPF.Brewery.Common.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly BreweryContext breweryContext;
        public BreweryRepository(BreweryContext breweryContext )
        {
            this.breweryContext = breweryContext;
        }

        public Models.DTO.Brewery? GetBrewery(int breweryId)
        {
            return breweryContext.Brewery.FirstOrDefault(b => b.Id == breweryId);
        }

        public Models.DTO.Brewery? GetBrewery(string breweryName)
        {
            return breweryContext.Brewery.FirstOrDefault(b => b.BreweryName == breweryName);
        }

        public IQueryable<Models.DTO.Brewery> GetBreweries()
        {
            return breweryContext.Brewery.AsQueryable();
        }

        public Models.DTO.Brewery? GetBreweryBeers(int breweryId)
        {
            return breweryContext.Brewery.Include(b => b.Beer)
                                         .Include("Beer.BeerType")
                                         .FirstOrDefault(b => b.Id == breweryId);
        }

        public IQueryable<Models.DTO.Brewery> GetAllBreweriesWithBeers()
        {
            return breweryContext.Brewery
                                 .Include(b => b.Beer)
                                 .Include("Beer.BeerType")
                                 .AsQueryable();
        }

        public int AddBrewery(Models.DTO.Brewery brewery)
        {
            breweryContext.Add(brewery);
            return breweryContext.SaveChanges();
        }

        public int UpdateBrewery(Models.DTO.Brewery brewery)
        {
            breweryContext.Update(brewery);
            return breweryContext.SaveChanges();
        }
    }
}