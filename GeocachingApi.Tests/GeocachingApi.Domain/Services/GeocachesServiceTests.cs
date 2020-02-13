using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeocachingApi.Domain.Services;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Tests.GeocachingApi.Domain.Services
{
    [TestClass]
    public class GeocachesServiceTests
    {
        private GeocachesService geocachesService;
        private Mock<IDataService> dataService;

        private IList<GeocacheModel> geocacheList;
        private GeocacheModel geocache;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dataService = new Mock<IDataService>();
            this.geocachesService = new GeocachesService(this.dataService.Object);

            this.SetupGeocaches();
        }

        [TestMethod]
        public async Task GetGeocaches_Should_Call_DataService_GetGeocaches()
        {
            this.dataService.Setup(x => x.GetGeocaches()).ReturnsAsync(this.geocacheList).Verifiable();
            var result = await this.geocachesService.GetGeocaches();
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task GetGeocaches_Should_Return_List_GeocacheModel_When_DataService_Returns_List()
        {
            this.dataService.Setup(x => x.GetGeocaches()).ReturnsAsync(this.geocacheList);
            var result = await this.geocachesService.GetGeocaches();
            Assert.AreEqual(typeof(List<GeocacheModel>), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocaches_Should_Return_List_GeocacheModel_When_DataService_Returns_Null()
        {
            this.dataService.Setup(x => x.GetGeocaches()).ReturnsAsync(() => null);
            var result = await this.geocachesService.GetGeocaches();
            Assert.AreEqual(typeof(List<GeocacheModel>), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocaches_Should_Return_Data_From_DataService_Getgeocaches()
        {
            this.dataService.Setup(x => x.GetGeocaches()).ReturnsAsync(this.geocacheList);
            var result = await this.geocachesService.GetGeocaches();
            Assert.AreEqual(this.geocacheList, result);
        }

        [TestMethod]
        public async Task GetGeocache_Should_Call_DataService_GetGeocache()
        {
            this.dataService.Setup(x => x.GetGeocache(1)).ReturnsAsync(this.geocache).Verifiable();
            var result = await this.geocachesService.GetGeocache(1);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task GetGeocache_Should_Return_GeocacheModel_When_DataService_Returns_Model()
        {
            this.dataService.Setup(x => x.GetGeocache(1)).ReturnsAsync(this.geocache);
            var result = await this.geocachesService.GetGeocache(1);
            Assert.AreEqual(typeof(GeocacheModel), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocache_Should_Return_GeocacheModel_When_DataService_Returns_Null()
        {
            this.dataService.Setup(x => x.GetGeocache(1)).ReturnsAsync(() => null);
            var result = await this.geocachesService.GetGeocache(1);
            Assert.AreEqual(typeof(GeocacheModel), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocache_Should_Return_Data_From_DataService_GetGeocache()
        {
            this.dataService.Setup(x => x.GetGeocache(1)).ReturnsAsync(this.geocache);
            var result = await this.geocachesService.GetGeocache(1);
            Assert.AreEqual(this.geocache, result);
        }

        private void SetupGeocaches()
        {
            this.geocacheList = new List<GeocacheModel>();
            this.geocache = new GeocacheModel
                            {
                                Id = 4,
                                Name = "Name"
                            };

            this.geocacheList.Add(this.geocache);
        }
    }
}
