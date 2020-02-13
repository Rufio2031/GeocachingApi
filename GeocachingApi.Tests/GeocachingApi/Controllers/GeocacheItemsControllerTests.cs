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
    public class GeocacheItemsServiceTests
    {
        private GeocacheItemsController geocacheItemsController;
        private Mock<IGeocacheItemsService> geocacheItemsService;
        private Mock<ILogger<GeocacheItemsController>> logger;

        private IList<GeocacheItemModel> geocacheItemList;
        private GeocacheItemModel geocacheItem;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocacheItemsService = new Mock<IGeocacheItemsService>();
            this.logger = new Mock<ILogger<GeocacheItemsController>>();
            this.geocacheItemsController = new GeocacheItemsController(this.logger.Object, this.geocacheItemsService.Object);

            this.SetupGeocacheItemList();
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_BadRequestObjectResult_If_Id_Is_Zero()
        {
            var result = await this.geocacheItemsController.GetGeocacheItem(0);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_BadRequestObjectResult_If_Id_Is_Negative()
        {
            var result = await this.geocacheItemsController.GetGeocacheItem(-1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Call_GeocacheItemsService_GetGeocacheItem()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItem(1)).ReturnsAsync(new GeocacheItemModel()).Verifiable();
            await this.geocacheItemsController.GetGeocacheItem(1);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_BadRequestObjectResult_If_GeocacheItemService_Throws()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItem(1)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.GetGeocacheItem(1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_NotFoundObjectResult_If_GeocacheItemService_Finds_No_Item()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItem(1)).ReturnsAsync(new GeocacheItemModel());
            var result = await this.geocacheItemsController.GetGeocacheItem(1);
            Assert.AreEqual(typeof(NotFoundObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_OkObjectResult_If_GeocacheItemService_Finds_Valid_Geocache_Item()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItem(1)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsController.GetGeocacheItem(1);
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_BadRequestObjectResult_If_GeocacheId_Is_Zero()
        {
            var result = await this.geocacheItemsController.GetGeocacheItemsByGeocacheId(0);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_BadRequestObjectResult_If_GeocacheId_Is_Less_Than_Zero()
        {
            var result = await this.geocacheItemsController.GetGeocacheItemsByGeocacheId(-1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Call_GeocacheItemService_GetGeocacheItemsByGeocacheId()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(new List<GeocacheItemModel>()).Verifiable();
            await this.geocacheItemsController.GetGeocacheItemsByGeocacheId(1);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_NotFoundObjectResult_When_No_Results_Found()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(new List<GeocacheItemModel>());
            var result = await this.geocacheItemsController.GetGeocacheItemsByGeocacheId(1);
            Assert.AreEqual(typeof(NotFoundObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_OkResult_When_Items_Are_Found()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(this.geocacheItemList);
            var result = await this.geocacheItemsController.GetGeocacheItemsByGeocacheId(1);
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_BadRequestObjectResult_When_GeocacheItemService_Throws_An_Exception()
        {
            this.geocacheItemsService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.GetGeocacheItemsByGeocacheId(1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Call_GeocacheItemsService_ValidateGeocacheItem()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>()).Verifiable();
            await this.geocacheItemsController.CreateGeocacheItem(this.geocacheItem);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Return_BadRequestObjectResult_When_ValidationMessages_Is_Not_Empty()
        {
            var testValidationMessages = new List<string> { "I am an error." };
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(testValidationMessages);
            var result = await this.geocacheItemsController.CreateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Call_GeocacheItemService_CreateGeocacheItem()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.CreateGeocacheItem(this.geocacheItem)).ReturnsAsync(this.geocacheItem).Verifiable();
            await this.geocacheItemsController.CreateGeocacheItem(this.geocacheItem);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Return_OkObjectResult_When_GeocacheItem_Creates_Successfully()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.CreateGeocacheItem(this.geocacheItem)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsController.CreateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Return_BadRequestObjectResult_When_GeocacheItem_ValidateGeocacheItem_Throws()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.CreateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Return_BadRequestObjectResult_When_GeocacheItem_CreateGeocacheItem_Throws()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.CreateGeocacheItem(this.geocacheItem)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.CreateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Call_GeocacheItemsService_ValidateGeocacheItem()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>()).Verifiable();
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Return_BadRequestObjectResult_When_ValidationMessages_Is_Not_Null()
        {
            var testValidationMessages = new List<string> { "I am an error." };
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(testValidationMessages);
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Return_BadRequestObjectResult_When_GeocacheItemsService_ValidateGeocacheItem_Throws()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Call_GeocacheItemsService_ValidateForUpdateGeocacheId()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.ValidateForUpdateGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(new List<string>()).Verifiable();
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Return_BadRequestObjectResult_When_GeocacheItemsService_ValidateForUpdateGeocacheId_Throws()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.ValidateForUpdateGeocacheId(1, this.geocacheItem.GeocacheId)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Return_BadRequestObjectResult_When_ValidationForUpdateMessages_Is_Not_Null()
        {
            var testValidationMessages = new List<string> { "I am an error." };
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.ValidateForUpdateGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(testValidationMessages);
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Call_GeocacheItemsService_UpdateGeocacheItemGeocacheId()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.ValidateForUpdateGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.UpdateGeocacheItemGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(this.geocacheItem).Verifiable();
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            this.geocacheItemsService.Verify();
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Return_OkObjectResult_When_GeocacheItemsService_UpdateGeocacheId_Succeeds()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.ValidateForUpdateGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.UpdateGeocacheItemGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateGeocacheId_Should_Return_BadRequestObjectResult_When_GeocacheItemsService_UpdateGeocacheId_Throws()
        {
            this.geocacheItemsService.Setup(x => x.ValidateGeocacheItem(this.geocacheItem)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.ValidateForUpdateGeocacheId(1, this.geocacheItem.GeocacheId)).ReturnsAsync(new List<string>());
            this.geocacheItemsService.Setup(x => x.UpdateGeocacheItemGeocacheId(1, this.geocacheItem.GeocacheId)).ThrowsAsync(new Exception());
            var result = await this.geocacheItemsController.UpdateGeocacheId(1, this.geocacheItem);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        private void SetupGeocacheItemList()
        {
            this.geocacheItemList = new List<GeocacheItemModel>();
            this.geocacheItem = new GeocacheItemModel { Id = 4 };

            this.geocacheItemList.Add(this.geocacheItem);
        }
    }
}
