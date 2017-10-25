namespace ProductService.Tests.Integration
{
    using System.Collections.Generic;
    using System.Web.Http.Results;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Models;
    using ProductService.Tests.DataStore;

    [TestFixture]
    public class ProductControllerLocalProductDataStoreIntegrationTests
    {
        private LocalProductDataStore _localProductDataStore;

        private ProductController _controller;

        [SetUp]
        public void SetUp()
        {
            this._localProductDataStore = new LocalProductDataStore();
            this._controller = new ProductController(this._localProductDataStore);
        }

        [Test]
        public void ShouldReturnContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems().GetAwaiter().GetResult();
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = TestData.GetItems();

            CollectionAssert.AreEqual(responseContents, result);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems().GetAwaiter().GetResult();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }
    }
}