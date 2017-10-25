namespace ProductService.Tests.DataStore
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Microsoft.Azure.Documents.Client;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Models;

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
            var itemsFromDataStore = _productDataStore.GetAllPlpItemsAsync();
            var actualData = itemsFromDataStore.Result.ToList();

            var expectedData = TestData.GetItems();

            CollectionAssert.AreEqual(actualData, expectedData);
        }
    }
}