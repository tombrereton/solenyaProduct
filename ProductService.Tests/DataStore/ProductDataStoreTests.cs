﻿namespace ProductService.Tests.DataStore
{
    using System.Configuration;

    using NUnit.Framework;

    using ProductService.DataStore;

    [TestFixture]
    public class ProductDataStoreTests
    {
        public ProductDataStore _productDataStore;

        [SetUp]
        public void SetUp()
        {
            string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];

            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(EndpointUrl, PrimaryKey);
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = _productDataStore.GetAllPlpItems();

            var expectedData = TestData.GetItems();

            CollectionAssert.AreEqual(itemsFromDataStore, expectedData);
        }
    }
}