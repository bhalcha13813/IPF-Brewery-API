using IPF.Brewery.Common;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IPF.Brewery.API.UnitTests.Repositories
{
    [TestFixture()]
    public class BeerRepositoryTests
    {
        private BreweryContext breweryContext;
        private IBeerRepository beerRepository;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<BreweryContext>().UseInMemoryDatabase("beer-ut-db");
            breweryContext = new BreweryContext(builder.Options);
            beerRepository = new BeerRepository(breweryContext);
        }

        [Test]
        public void Test_GetBeers_Returns_Beers()
        {
            BeerType beerType1 = new BeerType() { Id = 1, BeerTypeName = "bottle" };
            BeerType beerType2 = new BeerType() { Id = 2, BeerTypeName = "can" };

            breweryContext.Add(beerType1);
            breweryContext.Add(beerType2);

            Beer beer1 = new Beer() {Id = 1, BeerName = "Heineken", PercentageAlcoholByVolume = 4.3M, BeerTypeId = 1};
            Beer beer2 = new Beer() { Id = 2, BeerName = "Budweiser", PercentageAlcoholByVolume = 4.7M, BeerTypeId = 2};

            beerRepository.AddBeer(beer1);
            beerRepository.AddBeer(beer2);

            List<Beer> beers = beerRepository.GetBeers().ToList();

            Assert.IsNotEmpty(beers);
            Assert.AreEqual(2, beers.Count);
            Assert.IsNotNull(beers.Find(f => f.BeerName == beer1.BeerName));
            Assert.IsNotNull(beers.Find(f => f.BeerName == beer2.BeerName));
        }
    }
}
