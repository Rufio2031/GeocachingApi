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
    public class GeocacheItemsServiceTests
    {
        private GeocacheItemsService geocacheItemsService;
        private Mock<IDataService> dataService;
        private Mock<ILogger<GeocacheItemsService>> logger;

        private IList<GeocacheItemModel> geocacheItemList;
        private GeocacheItemModel geocacheItem;
        private IGeocacheItemPatchGeocacheIdModel patchGeocacheIdModel;

        [TestInitialize]
        public void TestInitialize()
        {
            this.dataService = new Mock<IDataService>();
            this.logger = new Mock<ILogger<GeocacheItemsService>>();
            this.geocacheItemsService = new GeocacheItemsService(this.dataService.Object, this.logger.Object);

            this.SetupGeocacheItems();
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Throw_ArgumentException_When_Id_Is_0()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.GetGeocacheItem(0));
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.GetGeocacheItem(-1));
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Call_DataService_GetGeocacheItem()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(1)).ReturnsAsync(new GeocacheItemModel()).Verifiable();
            await this.geocacheItemsService.GetGeocacheItem(1);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_GeocacheItem_From_DataService_GetGeocacheItem()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(1)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.GetGeocacheItem(1);
            Assert.AreEqual(this.geocacheItem.Id, result.Id);
        }

        [TestMethod]
        public async Task GetGeocacheItem_Should_Return_GeocacheItemModel_When_DataService_GetGeocacheItem_Returns_Null()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(1)).ReturnsAsync(() => null);
            var result = await this.geocacheItemsService.GetGeocacheItem(1);
            Assert.AreEqual(typeof(GeocacheItemModel), result.GetType());
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Throw_ArgumentException_When_Id_Is_0()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.GetGeocacheItemsByGeocacheId(0, true));
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.GetGeocacheItemsByGeocacheId(-1, true));
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Call_DataService_GetGeocacheItemsByGeocacheId()
        {
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(new List<GeocacheItemModel>()).Verifiable();
            await this.geocacheItemsService.GetGeocacheItemsByGeocacheId(1, true);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_List_GeocacheItemModel_When_DataService_GetGeocacheItemsByGeocacheId_Returns_List()
        {
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(this.geocacheItemList);
            var result = await this.geocacheItemsService.GetGeocacheItemsByGeocacheId(1, true);
            Assert.AreEqual(this.geocacheItemList, result);
        }

        [TestMethod]
        public async Task GetGeocacheItemsByGeocacheId_Should_Return_List_GeocacheItemModel_When_DataService_GetGeocacheItemsByGeocacheId_Returns_Null()
        {
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(() => null);
            var result = await this.geocacheItemsService.GetGeocacheItemsByGeocacheId(1, true);
            Assert.AreEqual(typeof(List<GeocacheItemModel>), result.GetType());
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Throw_ArgumentException_When_GeocacheItem_Is_Null()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.CreateGeocacheItem(null));
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Call_DataService_CreateGeocacheItem()
        {
            this.dataService.Setup(x => x.CreateGeocacheItem(this.geocacheItem)).ReturnsAsync(this.geocacheItem).Verifiable();
            await this.geocacheItemsService.CreateGeocacheItem(this.geocacheItem);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Return_GeocacheItem_When_DataService_CreateGeocacheItem_Returns_GeocacheItem()
        {
            this.dataService.Setup(x => x.CreateGeocacheItem(this.geocacheItem)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.CreateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(this.geocacheItem, result);
        }

        [TestMethod]
        public async Task CreateGeocacheItem_Should_Return_GeocacheItem_When_DataService_CreateGeocacheItem_Returns_Null()
        {
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(1, true)).ReturnsAsync(() => null);
            var result = await this.geocacheItemsService.CreateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(typeof(GeocacheItemModel), result.GetType());
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_Id_Is_0()
        {
            this.patchGeocacheIdModel.Id = 0;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_Id_Is_Negative()
        {
            this.patchGeocacheIdModel.Id = -1;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_GeocacheId_Is_0()
        {
            this.patchGeocacheIdModel.GeocacheId = 0;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Throw_ArgumentException_When_GeocacheId_Is_Negative()
        {
            this.patchGeocacheIdModel.GeocacheId = -1;
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Call_DataService_PatchGeocacheItemGeocacheId()
        {
            this.dataService.Setup(x => x.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel)).ReturnsAsync(this.geocacheItem).Verifiable();
            await this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Return_GeocacheItemModel_When_DataService_PatchGeocacheItemGeocacheId_Returns_GeocacheItemModel()
        {
            this.dataService.Setup(x => x.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel);
            Assert.AreEqual(this.geocacheItem, result);
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Return_GeocacheItemModel_When_DataService_PatchGeocacheItemGeocacheId_Returns_Null()
        {
            this.dataService.Setup(x => x.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel)).ReturnsAsync(() => null);
            var result = await this.geocacheItemsService.PatchGeocacheItemGeocacheId(this.patchGeocacheIdModel);
            Assert.AreEqual(typeof(GeocacheItemModel), result.GetType());
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Return_ValidationMessage_When_GeocacheItem_Is_Null()
        {
            var result = await this.geocacheItemsService.ValidateGeocacheItem(null);
            Assert.IsTrue(result.Contains("Invalid Geocache Item."));
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Return_ValidationMessage_When_GeocacheItem_ActiveStartDate_Is_GreaterThan_ActiveEndDate()
        {
            this.geocacheItem.ActiveStartDate = Convert.ToDateTime("3030-02-02");
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(true);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.IsTrue(result.Contains("Start Date cannot be after End Date."));
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Return_ValidationMessage_When_GeocacheItem_GeocacheId_Is_0()
        {
            this.geocacheItem.GeocacheId = 0;
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(true);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.IsTrue(result.Contains("Invalid GeocacheId."));
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Return_ValidationMessage_When_GeocacheItem_GeocacheId_Is_Negative()
        {
            this.geocacheItem.GeocacheId = -1;
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(true);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.IsTrue(result.Contains("Invalid GeocacheId."));
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Call_DataService_GetGeocacheItem()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem).Verifiable();
            await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Return_ValidationMessage_When_GeocacheItem_Name_Is_Already_In_Use()
        {
            var itemWithDuplicateName = new GeocacheItemModel
                                        {
                                            Id = 1,
                                            Name = "Name"
                                        };
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(itemWithDuplicateName);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(true);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.IsTrue(result.Contains("Geocache item name is already in use."));
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Not_Return_ValidationMessage_When_GeocacheItem_Name_Is_Already_In_Use_With_Same_Id_As_GeocacheItem_Passed_In()
        {
            var itemWithDuplicateName = new GeocacheItemModel
                                        {
                                            Id = 4,
                                            Name = "Name"
                                        };
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(itemWithDuplicateName);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(true);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.IsFalse(result.Contains("Geocache item name is already in use."));
        }

        [TestMethod]
        public async Task ValidateGeocacheItem_Should_Return_ValidationMessage_When_GeocacheItem_GeocacheId_Does_Not_Exist()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(false);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.IsTrue(result.Contains("Geocache does not exist."));
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Call_DataService_GeocacheIdExists_When_GeocacheId_Is_Greater_Than_0()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(false).Verifiable();
            await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Not_Call_DataService_GeocacheIdExists_When_GeocacheId_Is_0()
        {
            this.geocacheItem.GeocacheId = 0;
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            this.dataService.Verify(m => m.GeocacheIdExists(It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Not_Call_DataService_GeocacheIdExists_When_GeocacheId_Is_Negative()
        {
            this.geocacheItem.GeocacheId = -1;
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            this.dataService.Verify(m => m.GeocacheIdExists(It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public async Task PatchGeocacheItemGeocacheId_Should_Return_List_String_When_Validation_Passes()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(this.geocacheItem.Name)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GeocacheIdExists(this.geocacheItem.GeocacheId ?? 0)).ReturnsAsync(true);
            var result = await this.geocacheItemsService.ValidateGeocacheItem(this.geocacheItem);
            Assert.AreEqual(typeof(List<string>), result.GetType());
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_List_String_When_Validation_Passes()
        {
            this.patchGeocacheIdModel.GeocacheId = null;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.AreEqual(typeof(List<string>), result.GetType());
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_ValidationMessage_When_GeocacheItem_Does_Not_Exist()
        {
            var testItem = new GeocacheItemModel
            {
                Id = 0,
                Name = "Name",
                GeocacheId = null,
                ActiveStartDate = DateTime.MinValue,
                ActiveEndDate = DateTime.MaxValue
            };
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(testItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsTrue(result.Contains("Geocache Item does not exist."));
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Not_Return_ValidationMessage_Item_Is_Inactive_When_GeocacheItem_Does_Not_Exist()
        {
            var testItem = new GeocacheItemModel
                           {
                               Id = 0,
                               Name = "Name",
                               GeocacheId = null,
                               ActiveStartDate = DateTime.MinValue,
                               ActiveEndDate = DateTime.MaxValue
                           };
            this.geocacheItem.ActiveStartDate = DateTime.MinValue;
            this.geocacheItem.ActiveEndDate = DateTime.MinValue;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(testItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsFalse(result.Contains("Geocache Item is inactive."));
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_ValidationMessage_When_GeocacheItem_Is_Inactive()
        {
            this.geocacheItem.ActiveStartDate = DateTime.MinValue;
            this.geocacheItem.ActiveEndDate = DateTime.MinValue;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsTrue(result.Contains("Geocache Item is inactive."));
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_ValidationMessage_When_GeocacheId_Is_0()
        {
            this.patchGeocacheIdModel.GeocacheId = 0;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsTrue(result.Contains("Invalid GeocacheId."));
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_ValidationMessage_When_GeocacheId_Is_Negative()
        {
            this.patchGeocacheIdModel.GeocacheId = -1;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsTrue(result.Contains("Invalid GeocacheId."));
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Call_DataService_GetGeocacheItemsByGeocacheId_When_GeocacheId_Is_Not_Null()
        {
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(this.patchGeocacheIdModel.GeocacheId ?? 0, true))
                .ReturnsAsync(this.geocacheItemList).Verifiable();
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            this.dataService.Verify();
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Note_Call_DataService_GetGeocacheItemsByGeocacheId_When_GeocacheId_Is_Null()
        {
            this.patchGeocacheIdModel.GeocacheId = null;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            this.dataService.Verify(m => m.GetGeocacheItemsByGeocacheId(It.IsAny<int>(), It.IsAny<bool>()), Times.Never());
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Note_Call_DataService_GetGeocacheItemsByGeocacheId_When_GeocacheId_Is_0()
        {
            this.patchGeocacheIdModel.GeocacheId = 0;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            this.dataService.Verify(m => m.GetGeocacheItemsByGeocacheId(It.IsAny<int>(), It.IsAny<bool>()), Times.Never());
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Note_Call_DataService_GetGeocacheItemsByGeocacheId_When_GeocacheId_Is_Negative()
        {
            this.patchGeocacheIdModel.GeocacheId = -1;
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            this.dataService.Verify(m => m.GetGeocacheItemsByGeocacheId(It.IsAny<int>(), It.IsAny<bool>()), Times.Never());
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_ValidationMessage_When_Geocache_Already_Has_3_Items()
        {
            var existingGeocacheItems = new List<GeocacheItemModel>
                                        {
                                            new GeocacheItemModel(),
                                            new GeocacheItemModel(),
                                            new GeocacheItemModel()
                                        };
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(this.patchGeocacheIdModel.GeocacheId ?? 0, true))
                .ReturnsAsync(existingGeocacheItems);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsTrue(result.Contains("Cannot assign to Geocache with 3 or more active items."));
        }

        [TestMethod]
        public async Task ValidateForPatchGeocacheId_Should_Return_ValidationMessage_When_Geocache_Already_Has_More_Than_3_Items()
        {
            var existingGeocacheItems = new List<GeocacheItemModel>
                                        {
                                            new GeocacheItemModel(),
                                            new GeocacheItemModel(),
                                            new GeocacheItemModel(),
                                            new GeocacheItemModel()
                                        };
            this.dataService.Setup(x => x.GetGeocacheItem(this.patchGeocacheIdModel.Id)).ReturnsAsync(this.geocacheItem);
            this.dataService.Setup(x => x.GetGeocacheItemsByGeocacheId(this.patchGeocacheIdModel.GeocacheId ?? 0, true))
                .ReturnsAsync(existingGeocacheItems);
            var result = await this.geocacheItemsService.ValidateForPatchGeocacheId(this.patchGeocacheIdModel);
            Assert.IsTrue(result.Contains("Cannot assign to Geocache with 3 or more active items."));
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
