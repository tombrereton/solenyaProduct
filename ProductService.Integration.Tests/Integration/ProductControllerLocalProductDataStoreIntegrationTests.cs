using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.DataStore;
using ProductService.Models;
using ProductService.Integration.Tests.TestData;

namespace ProductService.Integration.Tests
{
    [TestFixture]
    public class ProductControllerLocalProductDataStoreIntegrationTests
    {
        private LocalProductDataStore _localProductDataStore;

        private ProductController _controller;

        [SetUp]
        public void SetUp()
        {
            _localProductDataStore = new LocalProductDataStore();
            _controller = new ProductController(_localProductDataStore);
        }

        [Test]
        public void ShouldReturnContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = _controller.GetItems();
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>) response).Content;

            // import items from json file and assign to variable
            var result = TestData.TestData.GetLocalItems();

            CollectionAssert.AreEqual(responseContents, result);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = _controller.GetItems();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }
    }
}