namespace ProductService.Tests.Adapter
{
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
            string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(EndpointUrl, PrimaryKey);

            TestData.SetUpDBWithTestData(this._productDataStore, this._collectionName);
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            TestData.TearDownDBTestData(this._productDataStore, this._collectionName);
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = _productDataStore.GetAllPlpItemsFromCollection(this._collectionName);
            var expectedData = TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(itemsFromDataStore, expectedData);
        }

        [Test]
        public void ReturnExactPdpItem()
        {
            var actualItemFromDataStore = this._productDataStore.GetPdpItemFromCollection(123, this._collectionName);
            var expectedItem = TestData.GeneratePdpItemTestData()[0];

            Assert.AreEqual(expectedItem, actualItemFromDataStore);
        }

        [Test]
        public void ReturnNullWhenPdpItemDoesNotExist()
        {
            var actualItemFromDataStore = this._productDataStore.GetPdpItemFromCollection(999, this._collectionName);
            object expected = null;

            Assert.AreEqual(expected, actualItemFromDataStore);
        }

        [Test]
        public void ReturnNullWhenCollectionDoesNotExist()
        {
            var actualItemfromDataStore = this._productDataStore.GetPdpItemFromCollection(123, "wrongCollectionName");
            object expected = null;

            Assert.AreEqual(expected, actualItemfromDataStore);
        }

        [Test]
        public void PopulateDB()
        {
            var itemsFromTestDb = TestData.GeneratePdpDbData();

            foreach (PdpItem pdpItem in itemsFromTestDb)
            {
                this._productDataStore.CreatePdpDocumentIfNotExists("products", pdpItem).Wait();
            }
        }
    }
}