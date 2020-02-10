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
    public class GeocacheControllerTests
    {
        private GeocacheController geocacheController;
        private Mock<IGeocacheService> geocacheService;
        private Mock<ILogger<GeocacheController>> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocacheService = new Mock<IGeocacheService>();
            this.logger = new Mock<ILogger<GeocacheController>>();
            this.geocacheController = new GeocacheController(this.logger.Object, this.geocacheService.Object);
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
