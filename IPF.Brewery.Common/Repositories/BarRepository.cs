using IPF.Brewery.Common.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace IPF.Brewery.Common.Repositories
{
    public class BarRepository : IBarRepository
    {
        private readonly BreweryContext breweryContext;
        public BarRepository(BreweryContext breweryContext )
        {
            this.breweryContext = breweryContext;
        }

        public Bar? getBar(int barId)
        {
            return breweryContext.Bar.FirstOrDefault(b => b.Id == barId);
        }

        public Bar? getBar(string barName)
        {
            return breweryContext.Bar.FirstOrDefault(b => b.BarName== barName);
        }

        public IQueryable<Bar> getBars()
        {
            return breweryContext.Bar.AsQueryable();
        }

        public IQueryable<Bar> getBarBeers(int barId)
        {
            return breweryContext.Bar.Include(b => b.Beer)
                .Where(b => b.Id == barId);
        }

        public IQueryable<Bar> getAllBarsWithBeers()
        {
            return breweryContext.Bar.Include(b => b.Beer).AsQueryable();
        }

        public int addBar(Bar bar)
        {
            breweryContext.Add(bar);
            return breweryContext.SaveChanges();
        }

        public int updateBar(Bar bar)
        {
            breweryContext.Update(bar);
            return breweryContext.SaveChanges();
        }
    }
}