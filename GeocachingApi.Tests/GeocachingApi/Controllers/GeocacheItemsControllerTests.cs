using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeocachingApi.Controllers;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Tests.GeocachingApi.Controllers
{
    [TestClass]
    public class GeocacheItemsControllerTests
    {
        private GeocacheItemController geocacheItemController;
        private Mock<IGeocacheItemService> geocacheItemService;
        private Mock<ILogger<GeocacheItemController>> logger;

        private IList<GeocacheItem> geocacheItemList;
        private GeocacheItem geocacheItem;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocacheItemService = new Mock<IGeocacheItemService>();
            this.logger = new Mock<ILogger<GeocacheItemController>>();
            this.geocacheItemController = new GeocacheItemController(this.logger.Object, this.geocacheItemService.Object);

            this.SetupGeocacheItemList();
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Return_BadRequest_If_GeocacheId_Is_Zero()
        {
            var result = await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(0);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Return_BadRequest_If_GeocacheId_Is_Less_Than_Zero()
        {
            var result = await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(-1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Call_GeocacheItemService_GetActiveGeocacheItemsByGeocacheId()
        {
            this.geocacheItemService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1)).ReturnsAsync(new List<GeocacheItem>()).Verifiable();
            await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(1);
            this.geocacheItemService.Verify();
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Return_NotFound_When_No_Results_Found()
        {
            this.geocacheItemService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1)).ReturnsAsync(new List<GeocacheItem>());
            var result = await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(1);
            Assert.AreEqual(typeof(NotFoundObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Return_OkResult_When_Items_Are_Found()
        {
            this.geocacheItemService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1)).ReturnsAsync(this.geocacheItemList);
            var result = await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(1);
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetActiveGeocacheItemsByGeocacheId_Should_Return_BadRequest_When_GeocacheItemService_Throws_An_Exception()
        {
            this.geocacheItemService.Setup(x => x.GetActiveGeocacheItemsByGeocacheId(1)).Throws(new Exception());
            var result = await this.geocacheItemController.GetActiveGeocacheItemsByGeocacheId(1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        private void SetupGeocacheItemList()
        {
            this.geocacheItemList = new List<GeocacheItem>();
            this.geocacheItem = new GeocacheItem();

            this.geocacheItemList.Add(this.geocacheItem);
        }
    }
}
