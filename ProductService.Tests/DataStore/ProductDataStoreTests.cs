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

        [SetUp]
        public void SetUp()
        {
            string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(EndpointUrl, PrimaryKey);

            TestData.TearDownDBTestData(this._productDataStore);
            TestData.SetUpDBWithTestData(this._productDataStore);
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var testCollection = "test_data_product";
            var itemsFromDataStore = _productDataStore.GetAllPlpItemsFromCollection(testCollection);
            var expectedData = TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(itemsFromDataStore, expectedData);
        }
    }
}