using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public class BeerTypeRepository : IBeerTypeRepository
    {
        private readonly BreweryContext breweryContext;

        public BeerTypeRepository(BreweryContext breweryContext )
        {
            this.breweryContext = breweryContext;
        }

        public IQueryable<BeerType> getBeerTypes()
        {
            return breweryContext.BeerType.AsQueryable();
        }

        public BeerType? getBeerType(int beerTypeId)
        {
            return breweryContext.BeerType.FirstOrDefault(b => b.Id == beerTypeId);
        }

        public BeerType? getBeerType(string beerType)
        {
            return breweryContext.BeerType.FirstOrDefault(b => b.BeerTypeName == beerType);
        }

        public int addBeerType(BeerType beerType)
        {
            breweryContext.Add(beerType);
            return breweryContext.SaveChanges();
        }

        public int updateBeerType(BeerType beerType)
        {
            breweryContext.Update(beerType);
            return breweryContext.SaveChanges();
        }
    }
}