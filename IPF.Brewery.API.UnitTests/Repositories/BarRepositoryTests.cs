using IPF.Brewery.Common;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IPF.Brewery.API.UnitTests.Repositories
{
    [TestFixture()]
    public class BarRepositoryTests
    {
        private BreweryContext breweryContext;
        private IBarRepository barRepository;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<BreweryContext>().UseInMemoryDatabase("bar-ut-db");
            breweryContext = new BreweryContext(builder.Options);
            barRepository = new BarRepository(breweryContext);
        }

        [Test]
        public void Test_GetBars_Returns_Bars()
        {
            Bar bar1 = new Bar() {Id = 1, BarName = "Mambo", Address = "Bridge Street, Taunton"};
            Bar bar2 = new Bar() { Id = 2, BarName = "Zinc", Address = "East Street, Taunton" };

            barRepository.addBar(bar1);
            barRepository.addBar(bar2);

            List<Bar> breweries = barRepository.getBars().ToList();

            Assert.IsNotEmpty(breweries);
            Assert.AreEqual(2, breweries.Count);
            Assert.IsNotNull(breweries.Find(f => f.BarName == bar1.BarName));
            Assert.IsNotNull(breweries.Find(f => f.BarName == bar2.BarName));
        }
    }
}
