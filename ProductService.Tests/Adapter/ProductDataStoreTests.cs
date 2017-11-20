namespace ProductService.Tests.Adapter
{
    using System.Collections.Generic;
    using System.Configuration;

    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Models;
    using ProductService.Tests.TestData;

    [TestFixture]
    public class ProductDataStoreTests
    {
        public ProductDataStore _productDataStore;

        private string _collectionName = "test_data_product";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            string endpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string primaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(endpointUrl, primaryKey);
            this._productDataStore.CreateDocumentCollection("empty_collection").Wait();

            TestData.SetUpDBWithTestData(this._productDataStore, this._collectionName);
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            TestData.TearDownDBTestData(this._productDataStore, this._collectionName);
            this._productDataStore.RemoveDocumentCollection("empty_collection").Wait();
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = this._productDataStore.GetAllPlpItemsFromCollection(this._collectionName);
            var expectedData = TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(itemsFromDataStore, expectedData);
        }

        [Test]
        public void ReturnProductIdOrCollectionErrorMsgWhenPlpCollectionDoesNotExist()
        {
            var actualItemfromDataStore = this._productDataStore.GetAllPlpItemsFromCollection("wrongCollection");
            var expected = new List<PlpItem>() { new PlpItem() { ProductName = "CollectionNameDoesNotExist" } };

            Assert.AreEqual(expected, actualItemfromDataStore);
        }

        [Test]
        public void ReturnExactPdpItem()
        {
            var actualItemFromDataStore = this._productDataStore.GetPdpItemFromCollection(123, this._collectionName);
            var expectedItem = TestData.GeneratePdpItemTestData()[0];

            Assert.AreEqual(expectedItem, actualItemFromDataStore);
        }

        [Test]
        public void ReturnProductIdErrorMsgWhenPdpItemDoesNotExist()
        {
            var actualItemFromDataStore = this._productDataStore.GetPdpItemFromCollection(999, this._collectionName);
            var expected = new PdpItem() { ProductName = "ProductItemDoesNotExist" };

            Assert.AreEqual(expected, actualItemFromDataStore);
        }

        [Test]
        public void ReturnProductIdOrCollectionErrorMsgWhenCollectionDoesNotExist()
        {
            var actualItemfromDataStore = this._productDataStore.GetPdpItemFromCollection(123, "wrongCollectionName");
            var expected = new PdpItem() { ProductName = "ProductItemOrCollectionNameDoesNotExist" };

            Assert.AreEqual(expected, actualItemfromDataStore);
        }

        [Test]
        public void ReturnCollectionEmptyErrorMsgWhenCollectionEmpty()
        {
            var actualItemfromDataStore = this._productDataStore.GetAllPlpItemsFromCollection("empty_collection");
            var expected = new List<PlpItem>();

            Assert.AreEqual(expected, actualItemfromDataStore);
        }
    }
}