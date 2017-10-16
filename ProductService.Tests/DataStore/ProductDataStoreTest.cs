using System.Collections.Generic;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.DataStore;
using ProductService.Models;
using ProductService.Tests.Controllers;

namespace ProductService.Tests.DataStore
{
    [TestFixture]
    public class ProductDataStoreTest
    {
        private Mock<IProductsDataStore> _productAdapter;

        private PlpController _plpController;

        private Mock<ITelemetryLogger> _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._productAdapter = new Mock<IProductsDataStore>();
            this._plpController = new PlpController(this._productAdapter.Object);
            this._telemetryLogger = new Mock<ITelemetryLogger>();
        }


        private static PlpItem CreateTestPlpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            return new PlpItem(id, productName, imageUrl, price, discountPrice);
        }
    }
}