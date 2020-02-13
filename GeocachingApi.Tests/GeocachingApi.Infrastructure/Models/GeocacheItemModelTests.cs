using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Tests.GeocachingApi.Infrastructure.Models
{
    [TestClass]
    public class GeocacheItemModelTests
    {
        private GeocacheItemModel geocacheItemModel;

        [TestInitialize]
        public void TestInitialize()
        {
            this.geocacheItemModel = new GeocacheItemModel
                                     {
                                         Id = 3,
                                         Name = "TestName",
                                         GeocacheId = 6,
                                         ActiveStartDate = DateTime.MinValue,
                                         ActiveEndDate = DateTime.MaxValue
                                     };
        }

        [TestMethod]
        public void GeocacheItemModel_Should_Set_IsActive_True_When_Date_Falls_Between_Active_Dates()
        {
            Assert.IsTrue(this.geocacheItemModel.IsActive);
        }

        [TestMethod]
        public void GeocacheItemModel_Should_Set_IsActive_False_When_Date_Falls_Outside_Active_Dates()
        {
            this.geocacheItemModel.ActiveStartDate = DateTime.MinValue;
            this.geocacheItemModel.ActiveEndDate = DateTime.MinValue;
            Assert.IsFalse(this.geocacheItemModel.IsActive);
        }
    }
}
