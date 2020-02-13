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
    public class GeocachesControllerTests
    {
        private GeocachesController geocachesController;
        private Mock<IGeocachesService> geocachesService;
        private Mock<ILogger<GeocachesController>> logger;

        private GeocacheModel geocache;
        private List<GeocacheModel> geocaches;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocachesService = new Mock<IGeocachesService>();
            this.logger = new Mock<ILogger<GeocachesController>>();
            this.geocachesController = new GeocachesController(this.logger.Object, this.geocachesService.Object);

            this.SetupGeocaches();
        }

        [TestMethod]
        public async Task Get_Should_Call_GeocachesService_GetGeocaches()
        {
            this.geocachesService.Setup(x => x.GetGeocaches()).ReturnsAsync(new List<GeocacheModel>()).Verifiable();
            await this.geocachesController.Get();
            this.geocachesService.Verify();
        }

        [TestMethod]
        public async Task Get_Should_Return_BadRequestObjectResult_When_GeocacheService_GetGeocaches_Throws_Exception()
        {
            this.geocachesService.Setup(x => x.GetGeocaches()).Throws(new Exception());
            var result = await this.geocachesController.Get();
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Return_NotFoundObjectResult_When_GeocacheService_GetGeocaches_Returns_No_Results()
        {
            this.geocachesService.Setup(x => x.GetGeocaches()).ReturnsAsync(new List<GeocacheModel>());
            var result = await this.geocachesController.Get();
            Assert.AreEqual(typeof(NotFoundObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Return_OkObjectResult_When_GeocacheService_GetGeocaches_Returns_No_Results()
        {
            this.geocachesService.Setup(x => x.GetGeocaches()).ReturnsAsync(this.geocaches);
            var result = await this.geocachesController.Get();
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Return_BadRequestObjectResult_When_Id_Is_Zero()
        {
            var result = await this.geocachesController.Get(0);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Return_BadRequestObjectResult_When_Id_Is_Negative()
        {
            var result = await this.geocachesController.Get(-1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Call_GeocachesService_GetGeocache()
        {
            this.geocachesService.Setup(x => x.GetGeocache(1)).ReturnsAsync(new GeocacheModel()).Verifiable();
            await this.geocachesController.Get(1);
            this.geocachesService.Verify();
        }

        [TestMethod]
        public async Task Get_Should_Return_NotFoundObjectResult_When_GeocachesService_GetGeocache_Returns_No_Result()
        {
            this.geocachesService.Setup(x => x.GetGeocache(1)).ReturnsAsync(new GeocacheModel());
            var result = await this.geocachesController.Get(1);
            Assert.AreEqual(typeof(NotFoundObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Return_OkObjectResult_When_GeocachesService_GetGeocache_Returns_Result()
        {
            this.geocachesService.Setup(x => x.GetGeocache(1)).ReturnsAsync(this.geocache);
            var result = await this.geocachesController.Get(1);
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
        }

        [TestMethod]
        public async Task Get_Should_Return_BadRequestObjectResult_When_GeocachesService_GetGeocache_Throws_Exception()
        {
            this.geocachesService.Setup(x => x.GetGeocache(1)).ThrowsAsync(new Exception());
            var result = await this.geocachesController.Get(1);
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        private void SetupGeocaches()
        {
            this.geocache = new GeocacheModel
            {
                Id = 1,
                Name = "Name",
                Location = new GeocacheLocationModel()
            };

            this.geocaches = new List<GeocacheModel>
            {
                this.geocache,
                new GeocacheModel(),
                new GeocacheModel()
            };
        }
    }
}
