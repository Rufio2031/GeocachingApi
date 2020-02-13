using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeocachingApi.Domain.Services;
using GeocachingApi.Domain.DataAccess.Geocaching;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Tests.GeocachingApi.Domain.Services
{
    [TestClass]
    public class DataServiceTests
    {
        private DataService dataService;
        private Mock<geocachingContext> dbContext;

        private IList<GeocacheItemModel> geocacheItemList;
        private GeocacheItemModel geocacheItem;
        private IGeocacheItemPatchGeocacheIdModel patchGeocacheIdModel;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dbContext = new Mock<geocachingContext>();
            this.dataService = new DataService(this.dbContext.Object);

            this.SetupGeocacheItems();
        }

        [TestMethod]
        public async Task GetGeocaches_Should_Throw_ArgumentException_When_Id_Is_Zero()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocache(0));
        }

        [TestMethod]
        public async Task GetGeocaches_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocache(-1));
        }

        [TestMethod]
        public async Task GeocacheIdExists_Should_Throw_ArgumentException_When_Id_Is_Zero()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GeocacheIdExists(0));
        }

        [TestMethod]
        public async Task GeocacheIdExists_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GeocacheIdExists(-1));
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Throw_ArgumentException_When_Id_Is_Zero()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocacheItemsByGeocacheId(0, true));
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocacheItemsByGeocacheId(-1, true));
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Throw_ArgumentException_When_Id_Is_Zero()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocacheItem(0));
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocacheItem(-1));
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Throw_ArgumentException_When_Name_Is_Null()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocacheItem(null));
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Throw_ArgumentException_When_Name_Is_Whitespace()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.GetGeocacheItem(" "));
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Throw_ArgumentException_When_Name_Is_Null()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.CreateGeocacheItem(null));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_Id_Is_Zero()
        {
            this.patchGeocacheIdModel.Id = 0;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            this.patchGeocacheIdModel.Id = -1;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_GeocacheId_Is_Zero()
        {
            this.patchGeocacheIdModel.GeocacheId = 0;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_GeocacheId_Is_Negative()
        {
            this.patchGeocacheIdModel.GeocacheId = -1;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.dataService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        private void SetupGeocacheItems()
        {
            this.geocacheItemList = new List<GeocacheItemModel>();
            this.geocacheItem = new GeocacheItemModel
                                {
                                    Id = 4,
                                    Name = "Name",
                                    GeocacheId = 4,
                                    ActiveStartDate = Convert.ToDateTime("2020-02-02"),
                                    ActiveEndDate = Convert.ToDateTime("2022-02-02")
            };

            this.geocacheItemList.Add(this.geocacheItem);

            this.patchGeocacheIdModel = new GeocacheItemPatchGeocacheIdModel
                                        {
                                            Id = 3,
                                            GeocacheId = 6
                                        };
        }
    }
}
