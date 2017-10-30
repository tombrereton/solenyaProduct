namespace ProductService.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Moq;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.Models;
    using ProductService.Tests.TestData;

    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductsDataStore> _productAdapter;

        private ProductController _productController;

        private Mock<ITelemetryLogger> _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._productAdapter = new Mock<IProductsDataStore>();
            this._productController = new ProductController(this._productAdapter.Object);
            this._telemetryLogger = new Mock<ITelemetryLogger>();
        }

        [Test]
        public void ReturnOkResponseForGetRequest()
        {
            var plpItems = new List<PlpItem> { TestData.CreateTestPlpItem(123), TestData.CreateTestPlpItem(345) };
            this._productAdapter.Setup(x => x.GetAllPlpItemsFromCollection("products")).Returns(plpItems);

            var result = this._productController.GetItems();

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        [Test]
        public void ReturnNonEmptyListOfProducts()
        {
            var plpItems = new List<PlpItem> { TestData.CreateTestPlpItem(123), TestData.CreateTestPlpItem(345) };

            this._productAdapter.Setup(x => x.GetAllPlpItemsFromCollection("products")).Returns(plpItems);

            var result = this._productController.GetItems();

            var resultItems = ((OkNegotiatedContentResult<List<PlpItem>>)result).Content;

            Assert.NotNull(resultItems);
        }

        [Test]
        public void ReturnListOfItems()
        {
            var plpItems = new List<PlpItem> { TestData.CreateTestPlpItem(123), TestData.CreateTestPlpItem(345) };

            this._productAdapter.Setup(x => x.GetAllPlpItemsFromCollection("products")).Returns(plpItems);

            var result = this._productController.GetItems();

            var resultItems = ((OkNegotiatedContentResult<List<PlpItem>>)result).Content;

            Assert.That(resultItems, Is.EqualTo(plpItems));
        }

        [Test]
        public async Task ReturnNotFoundResultWhenDatastoreReturnsNoProducts()
        {
            this._productAdapter.Setup(x => x.GetAllPlpItemsFromCollection("test_data_product"))
                .Returns(new List<PlpItem>());

            var result = this._productController.GetItems();

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        // Add test to check if data has been hard coded

        [Test]
        public void ReturnOnePdpItemById()
        {
            var pdpItem = new List<PdpItem> {TestData.CreateTestPdpItem(123) };

            this._productAdapter.Setup(x => x.GetPdpItemFromCollection("products", 123)).Returns(pdpItem);

            var result = this._productController.GetItem(123);

            var resultItem = ((OkNegotiatedContentResult<List<PdpItem>>)result).Content;

            Assert.That(resultItem, Is.EqualTo(pdpItem));

        }

        [Test]
        public void ReturnNotFoundResponseIfProductNotFound()
        {
            this._productAdapter.Setup(x => x.GetPdpItemFromCollection("test_data_product", 123))
                .Returns(new List<PdpItem>());

            var result = this._productController.GetItem(123);

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Test]
        public void ReturnsNotNullObjectWhenRequestedByValidId()
        {
            var pdpItem = new List<PdpItem> { TestData.CreateTestPdpItem(123) };

            this._productAdapter.Setup(x => x.GetPdpItemFromCollection("products", 123)).Returns(pdpItem);

            var result = this._productController.GetItem(123);

            var resultItem = ((OkNegotiatedContentResult<List<PdpItem>>)result).Content;

            Assert.NotNull(resultItem);
        }

        [Test]
        public void ReturnsOkResponseIfProductIsFoundById()
        {
            var pdpItem = new List<PdpItem> { TestData.CreateTestPdpItem(123) };
            this._productAdapter.Setup(x => x.GetPdpItemFromCollection("products", 123)).Returns(pdpItem);

            var result = this._productController.GetItem(123);

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<PdpItem>>>());
        }
    }
}