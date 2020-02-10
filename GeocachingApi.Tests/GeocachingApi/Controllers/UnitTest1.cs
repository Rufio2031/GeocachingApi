using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeocachingApi.Controllers;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;


namespace GeocachingApi.Tests
{
    [TestClass]
    public class UnitTest1
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
        public async void GetActiveGeocacheItemsByGeocacheId_Should_Call_GeocacheItemService_GetActiveGeocacheItemsByGeocacheId()
        {
            //this.geocacheItemService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1));
            await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(1);
            this.geocacheItemService.Verify(x => x.GetActiveGeocacheItemsByGeocacheId(1));
        }
    }
}
