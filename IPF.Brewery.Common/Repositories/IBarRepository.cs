using IPF.Brewery.Common.Models.DTO;

namespace IPF.Brewery.Common.Repositories
{
    public interface IBarRepository
    {
        Bar? getBar(int barId);
        Bar? getBar(string barName);
        IQueryable<Bar> getBars();
        Bar? getBarBeers(int barId);
        IQueryable<Bar> getAllBarsWithBeers();

        int addBar(Bar bar);
        int updateBar(Bar bar);
    }
}
