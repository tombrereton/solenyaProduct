namespace ProductService.Tests.Integration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Models;
    using ProductService.Tests.DataStore;
    using ProductService.Tests.TestData;

    [TestFixture]
    public class ProductControllerProductDataStoreIntegrationTests
    {
        private ProductDataStore _productDataStore;

        private ProductController _controller;

        private IEnumerable<PlpItem> _testItem;

        private readonly string _collectionName = "test_data_product";

        [SetUp]
        public void SetUp()
        {
            string endpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string primaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(endpointUrl, primaryKey);
            this._controller = new ProductController(this._productDataStore);

            TestData.TearDownDBTestData(this._productDataStore);
            TestData.SetUpDBWithTestData(this._productDataStore);
        }

        [Test]
        public void ShouldReturnContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems(this._collectionName);
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = TestData.GeneratePlpItemTestData();

            CollectionAssert.AreEqual(result, responseContents);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems(this._collectionName);

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        // [Test]
        // public void ShouldReturnDataMatchingDataFromTestData()
        // {
        // }
    }
}