namespace ProductService.Tests.DataStore
{
    using System.Configuration;

    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Tests.TestData;

    [TestFixture]
    public class ProductDataStoreTests
    {
        public ProductDataStore _productDataStore;

        private string _collectionName = "test_data_product";

        [SetUp]
        public void SetUp()
        {
            string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(EndpointUrl, PrimaryKey);

            TestData.TearDownDBTestData(this._productDataStore, this._collectionName);
            TestData.SetUpDBWithTestData(this._productDataStore, this._collectionName);
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = _productDataStore.GetAllPlpItemsFromCollection(this._collectionName);
            var expectedData = TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(itemsFromDataStore, expectedData);
        }

        [Test]
        public void PopulateDatabase()
        {
        }
    }
}