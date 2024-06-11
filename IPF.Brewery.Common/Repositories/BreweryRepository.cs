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

        public Models.DTO.Brewery? getBrewery(int breweryId)
        {
            return breweryContext.Brewery.FirstOrDefault(b => b.Id == breweryId);
        }

        public Models.DTO.Brewery? getBrewery(string breweryName)
        {
            return breweryContext.Brewery.FirstOrDefault(b => b.BreweryName == breweryName);
        }

        public IQueryable<Models.DTO.Brewery> getBreweries()
        {
            return breweryContext.Brewery.AsQueryable();
        }

        public Models.DTO.Brewery? getBreweryBeers(int breweryId)
        {
            return breweryContext.Brewery.Include(b => b.Beer)
                .FirstOrDefault(b => b.Id == breweryId);
        }

        public IQueryable<Models.DTO.Brewery> getAllBreweriesWithBeers()
        {
            return breweryContext.Brewery.Include(b => b.Beer);
        }

        public int addBrewery(Models.DTO.Brewery brewery)
        {
            breweryContext.Add(brewery);
            return breweryContext.SaveChanges();
        }

        public int updateBrewery(Models.DTO.Brewery brewery)
        {
            breweryContext.Update(brewery);
            return breweryContext.SaveChanges();
        }
    }
}