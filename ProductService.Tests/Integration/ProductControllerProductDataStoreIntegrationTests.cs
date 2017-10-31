namespace ProductService.Tests.Integration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Http.Results;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Models;
    using ProductService.Tests.TestData;

    [TestFixture]
    public class ProductControllerProductDataStoreIntegrationTests
    {
        private ProductDataStore _productDataStore;

        private ProductController _controller;

        private IEnumerable<PlpItem> _testItem;

        private readonly string _collectionName = "test_data_product";

        [SetUp]
        public void SetUp()
        {
            string endpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string primaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(endpointUrl, primaryKey);
            this._controller = new ProductController(this._productDataStore);

            TestData.TearDownDBTestData(this._productDataStore, this._collectionName);
            TestData.SetUpDBWithTestData(this._productDataStore, this._collectionName);
        }

        [Test]
        public void ShouldReturnPlpContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems(this._collectionName);
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(result, responseContents);
        }

        [Test]
        public void ShouldReturnPdpContentMatchingItemFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItem(123, this._collectionName);
            var responseContents = ((OkNegotiatedContentResult<PdpItem>)response).Content;

            var result = TestData.GenerateSinglePdpItemTestData();

            Assert.AreEqual(result, responseContents);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems(this._collectionName);

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        [Test]
        public void ShouldReturnPdpItemFromControllerAndDatastore()
        {
            var actualResponse = this._controller.GetItem(123, this._collectionName);
            var actualOkNegotiatedContent = actualResponse as OkNegotiatedContentResult<PdpItem>;
            var actualContent = actualOkNegotiatedContent.Content;

            var expectedPdpItem = TestData.GeneratePdpItemTestData()[2];

            Assert.AreEqual(expectedPdpItem, actualContent);
        }

        [Test]
        public void ShouldReturnOkResponseForPdpItemWithControllerAndDataStore()
        {
            var response = this._controller.GetItem(123, this._collectionName);

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<PdpItem>>());
        }

        [Test]
        public void ShouldReturnNotFoundResponseForIncorrectProductId()
        {
            var actualItemFromController = this._controller.GetItem(999, this._collectionName);

            Assert.IsInstanceOf(typeof(NotFoundResult), actualItemFromController);
        }

        [Test]
        public void ShouldReturnNotFoundResponseForIncorrectCollectionName()
        {
            var actualItemFromController = this._controller.GetItem(123, "wrongCollectionName");

            Assert.IsInstanceOf(typeof(NotFoundResult), actualItemFromController);
        }
    }
}