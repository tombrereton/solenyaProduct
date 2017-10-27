namespace ProductService.Tests.Integration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Models;
    using ProductService.Tests.DataStore;

    [TestFixture]
    public class ProductControllerProductDataStoreIntegrationTests
    {
        private ProductDataStore _productDataStore;

        private ProductController _controller;

        private ResourceSetUp _resourceSetUp;

        private IEnumerable<PlpItem> _testItem;

        [SetUp]
        public async Task RSetUp()
        {
            this._controller = new ProductController(new ProductDataStore());
            this._testItem = TestData.GetItems();
            this._resourceSetUp = new ResourceSetUp();
            await this._resourceSetUp.SetUpDb();
           // await this._resourceSetUp.TearDown();
        }

        [SetUp]
        public void SetUp()
        {
            string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];

            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

            this._productDataStore = new ProductDataStore(EndpointUrl, PrimaryKey);
            this._controller = new ProductController(this._productDataStore);
        }

        [Test]
        public void ShouldReturnContentsMatchingListFromJsonWithControllerAndDatastore()
        {
            var response = this._controller.GetItems();
            var responseContents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = TestData.GetItems();

            CollectionAssert.AreEqual(result, responseContents);
        }

        [Test]
        public void ShouldReturnAllItemsWithControllerAndDatastore()
        {
            var response = this._controller.GetItems();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }
//
//        [Test]
//        public void ShouldReturnDataMatchingDataFromTestData()
//        {
//        }
    }
}