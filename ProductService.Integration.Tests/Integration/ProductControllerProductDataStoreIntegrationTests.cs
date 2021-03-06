﻿namespace ProductService.Integration.Tests
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Http.Results;
    using System.Xml.Serialization;
    using Moq;
    using NUnit.Framework;
    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Logger;
    using ProductService.Models;

    [TestFixture]
    public class ProductControllerProductDataStoreIntegrationTests
    {
        private ProductDataStore _productDataStore;

        private ProductController _controller;

        private IEnumerable<PlpItem> _testItem;

        private Mock<ITelemetryLogger> _telemetryLogger;

        private readonly string _collectionName = "test_data_product";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(EndpointUrl, PrimaryKey);
            this._telemetryLogger = new Mock<ITelemetryLogger>();
            this._controller = new ProductController(this._productDataStore);

            this._productDataStore.CreateDocumentCollection("empty_collection");

            TestData.TestData.SetUpDBWithTestData(this._productDataStore, this._collectionName);
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            TestData.TestData.TearDownDBTestData(this._productDataStore, this._collectionName);
            this._productDataStore.RemoveDocumentCollection("empty_collection");
        }

        [Test]
        public void ShouldReturnPlpContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems(this._collectionName);
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>) response).Content;

            // import items from json file and assign to variable
            var expected = TestData.TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(expected, responseContents);
        }

        [Test]
        public void ShouldReturnPdpContentMatchingItemFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItem(123, this._collectionName);
            var responseContents = ((OkNegotiatedContentResult<PdpItem>) response).Content;

            var result = TestData.TestData.GeneratePdpItemTestData()[0];

            Assert.AreEqual(result, responseContents);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems(this._collectionName);

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        [Test]
        public void ShouldReturnCollectionErrorMsgWhenPlpCollectionNotFoundWithControllerAndDatastore()
        {
            var actualItemFromController = this._controller.GetItems("wrongCollection");

            Assert.That(actualItemFromController, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>) actualItemFromController;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("CollectionNameDoesNotExist"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Collection name was not found in the database."));
        }

        [Test]
        public void ShouldReturnCollectionEmptyErrorMsgWhenPlpCollectionNotFoundWithControllerAndDatastore()
        {
            var actualItemFromController = this._controller.GetItems("empty_collection");

            Assert.That(actualItemFromController, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>) actualItemFromController;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("CollectionEmpty"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Collection does not contain any product items in the database."));
        }

        [Test]
        public void ShouldReturnPdpItemFromControllerAndDatastore()
        {
            var actualResponse = this._controller.GetItem(123, this._collectionName);
            var actualOkNegotiatedContent = actualResponse as OkNegotiatedContentResult<PdpItem>;
            var actualContent = actualOkNegotiatedContent.Content;

            var expectedPdpItem = TestData.TestData.GeneratePdpItemTestData()[0];

            Assert.AreEqual(expectedPdpItem, actualContent);
        }

        [Test]
        public void ShouldReturnOkResponseForPdpItemWithControllerAndDataStore()
        {
            var response = this._controller.GetItem(123, this._collectionName);

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<PdpItem>>());
        }

        [Test]
        public void ShouldReturnErrorIfProductIdIsInvalid()
        {
            var actualItemFromController = this._controller.GetItem(999, this._collectionName);

            Assert.That(actualItemFromController, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>) actualItemFromController;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("ProductItemDoesNotExist"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Product item was not found in the database."));
        }

        [Test]
        public void ShouldReturnErrorForIncorrectCollectionName()
        {
            var actualItemFromController = this._controller.GetItem(123, string.Empty);

            Assert.That(actualItemFromController, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>) actualItemFromController;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("ProductItemOrCollectionNameDoesNotExist"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Product item or collection name was not found in the database."));
        }
    }
}