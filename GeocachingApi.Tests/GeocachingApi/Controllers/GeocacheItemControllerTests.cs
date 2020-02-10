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
    public class GeocacheItemControllerTests
    {
        private GeocacheItemController geocacheItemController;
        private Mock<IGeocacheItemService> geocacheItemService;
        private Mock<ILogger<GeocacheItemController>> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocacheItemService = new Mock<IGeocacheItemService>();
            this.logger = new Mock<ILogger<GeocacheItemController>>();
            this.geocacheItemController = new GeocacheItemController(this.logger.Object, this.geocacheItemService.Object);
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Call_GeocacheItemService_GetActiveGeocacheItemsByGeocacheId()
        {
            this.geocacheItemService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1)).ReturnsAsync(new List<GeocacheItem>()).Verifiable();
            await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(1);
            this.geocacheItemService.Verify();
        }
    }
}
