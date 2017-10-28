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

        [SetUp]
        public void SetUp()
        {
            string endpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];
            string primaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(endpointUrl, primaryKey);
            this._controller = new ProductController(this._productDataStore);

            TestData.TearDownDBTestData(this._productDataStore).Wait();
            TestData.SetUpDBWithTestData(this._productDataStore).Wait();
        }

        [Test]
        public void ShouldReturnContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems();
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = TestData.GetDBItems();

            CollectionAssert.AreEqual(result, responseContents);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        // [Test]
        // public void ShouldReturnDataMatchingDataFromTestData()
        // {
        // }
    }
}