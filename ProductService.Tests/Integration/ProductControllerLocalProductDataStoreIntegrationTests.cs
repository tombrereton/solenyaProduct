﻿namespace ProductService.Tests.Integration
{
    using System.Collections.Generic;
    using System.Web.Http.Results;

    using Moq;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Logger;
    using ProductService.Models;
    using ProductService.Tests.Adapter;
    using ProductService.Tests.Controllers;
    using ProductService.Tests.TestData;

    [TestFixture]
    public class ProductControllerLocalProductDataStoreIntegrationTests
    {
        private LocalProductDataStore _localProductDataStore;

        private ProductController _controller;

        private Mock<ITelemetryLogger> _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._localProductDataStore = new LocalProductDataStore();
            this._telemetryLogger = new Mock<ITelemetryLogger>();
            this._controller = new ProductController(this._localProductDataStore, this._telemetryLogger.Object);
        }

        [Test]
        public void ShouldReturnContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems();
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = TestData.GetLocalItems();

            CollectionAssert.AreEqual(responseContents, result);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }
    }
}