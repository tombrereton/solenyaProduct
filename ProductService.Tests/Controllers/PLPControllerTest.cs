using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductService;
using ProductService.Controllers;
using Moq;
using NUnit.Framework;

namespace ProductService.Tests.Controllers
{
    [TestFixture]
    public class PLPControllerTest
    {
        private Mock<IProductsDataStore> ProductAdapter;

        private PLPController PLPController;

        private Mock<ITelemetryLogger> TelemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this.ProductAdapter = new Mock<IProductsDataStore>();
            this.PLPController = new PLPController(this.ProductAdapter.Object);
            this.TelemetryLogger = new Mock<ITelemetryLogger>();
        }

        [Test]
        public void Return_bad_request_if_no_product()
        {
            var PLPItem = CreateTestPLPItem(123);
            PLPItem.ProductName = null;
        }
        
        private static PLPItem CreateTestPLPItem(int ID)
        {
            string ProductName = "Test Product";
            string ImageURL = "Test URL";
            int Price = 2000;
            int DiscountPrice = 1500;
            return new PLPItem(ID, ProductName, ImageURL, Price, DiscountPrice);
        }
    }
}