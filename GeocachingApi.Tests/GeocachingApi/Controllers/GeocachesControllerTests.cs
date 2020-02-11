using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeocachingApi.Controllers;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Tests.GeocachingApi.Controllers
{
    [TestClass]
    public class GeocachesControllerTests
    {
        private GeocachesController geocacheController;
        private Mock<IGeocachesService> geocacheService;
        private Mock<ILogger<GeocachesController>> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocacheService = new Mock<IGeocachesService>();
            this.logger = new Mock<ILogger<GeocachesController>>();
            this.geocacheController = new GeocachesController(this.logger.Object, this.geocacheService.Object);
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Call_GeocacheItemService_GetActiveGeocacheItemsByGeocacheId()
        {
            //this.geocacheService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1)).ReturnsAsync(new List<GeocacheItem>()).Verifiable();
            //await this.geocacheController.GetActiveGeocacheItemsByGeocacheId(1);
            //this.geocacheService.Verify();
        }
    }
}
