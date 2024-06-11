using IPF.Brewery.Common;
using IPF.Brewery.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IPF.Brewery.API.UnitTests.Repositories
{
    [TestFixture()]
    public class BreweryRepositoryTests
    {
        private BreweryContext breweryContext;
        private IBreweryRepository breweryRepository;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<BreweryContext>().UseInMemoryDatabase("brewery-ut-db");
            breweryContext = new BreweryContext(builder.Options);
            breweryRepository = new BreweryRepository(breweryContext);
        }

        [Test]
        public void Test_GetBreweries_Returns_Breweries()
        {
            Common.Models.DTO.Brewery brewery1 = new Common.Models.DTO.Brewery() {Id = 1, BreweryName = "Kirkstall Brewery", Address = "Kirkstall Road, Leeds"};
            Common.Models.DTO.Brewery brewery2 = new Common.Models.DTO.Brewery() { Id = 2, BreweryName = "Bushmills Brewery", Address = "Marsh Lane, Belfast" };

            breweryRepository.addBrewery(brewery1);
            breweryRepository.addBrewery(brewery2);

            List<Common.Models.DTO.Brewery> breweries = breweryRepository.getBreweries().ToList();

            Assert.IsNotEmpty(breweries);
            Assert.AreEqual(2, breweries.Count);
            Assert.IsNotNull(breweries.Find(f => f.BreweryName == brewery1.BreweryName));
            Assert.IsNotNull(breweries.Find(f => f.BreweryName == brewery2.BreweryName));
        }
    }
}
