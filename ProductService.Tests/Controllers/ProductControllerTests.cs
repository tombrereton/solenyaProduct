namespace ProductService.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Moq;

    using NUnit.Framework;

    using ProductService.Controllers;
    using ProductService.DataStore;
    using ProductService.ErrorHandler;
    using ProductService.Logger;
    using ProductService.Models;
    using ProductService.Tests.TestData;

    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductsDataStore> _dataStore;

        private ProductController _productController;

        private Mock<ITelemetryLogger> _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._dataStore = new Mock<IProductsDataStore>();
            this._telemetryLogger = new Mock<ITelemetryLogger>();
            this._productController = new ProductController(this._dataStore.Object);
        }

        [Test]
        public void ReturnOkResponseForPlpGetRequest()
        {
            var plpItems = new List<PlpItem> { TestData.CreateTestPlpItem(123), TestData.CreateTestPlpItem(345) };
            this._dataStore.Setup(x => x.GetAllPlpItemsFromCollection("products")).Returns(plpItems);

            var result = this._productController.GetItems();

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        [Test]
        public void ReturnNonEmptyListOfProductsForPlp()
        {
            var plpItems = new List<PlpItem> { TestData.CreateTestPlpItem(123), TestData.CreateTestPlpItem(345) };

            this._dataStore.Setup(x => x.GetAllPlpItemsFromCollection("products")).Returns(plpItems);

            var result = this._productController.GetItems();

            var resultItems = ((OkNegotiatedContentResult<List<PlpItem>>)result).Content;

            Assert.NotNull(resultItems);
        }

        [Test]
        public void ReturnListOfItems()
        {
            var plpItems = new List<PlpItem> { TestData.CreateTestPlpItem(123), TestData.CreateTestPlpItem(345) };

            this._dataStore.Setup(x => x.GetAllPlpItemsFromCollection("products")).Returns(plpItems);

            var result = this._productController.GetItems();

            var resultItems = ((OkNegotiatedContentResult<List<PlpItem>>)result).Content;

            Assert.That(resultItems, Is.EqualTo(plpItems));
        }

        [Test]
        public async Task ReturnCollectionErrorMsgWhenDatastoreReturnsNoProductsForPlp()
        {
            var items = new List<PlpItem>() { new PlpItem() { ProductName = ErrorCodes.CollectionNotFoundCode } };
            this._dataStore.Setup(x => x.GetAllPlpItemsFromCollection("wrongCollection")).Returns(items);

            var result = this._productController.GetItems("wrongCollection");

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>)result;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("CollectionNameDoesNotExist"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Collection name was not found in the database."));
        }

        [Test]
        public async Task ReturnCollectionEmptyErrorMsgWhenDatastoreReturnsNoProductsForPlp()
        {
            var items = new List<PlpItem>();
            this._dataStore.Setup(x => x.GetAllPlpItemsFromCollection("emptyCollection")).Returns(items);

            var result = this._productController.GetItems("emptyCollection");

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>)result;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("CollectionEmpty"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Collection does not contain any product items in the database."));
        }

        [Test]
        public void ReturnOnePdpItemById()
        {
            var pdpItem = TestData.CreateTestPdpItem(123);

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(123, "products")).Returns(pdpItem);

            var result = this._productController.GetItem(123);

            var resultItem = ((OkNegotiatedContentResult<PdpItem>)result).Content;

            Assert.That(resultItem, Is.EqualTo(pdpItem));
        }

        [Test]
        public void ReturnProductNotFoundErrorMsgIfProductIdIsInvalid()
        {
            var pdpItem = new PdpItem() { ProductName = ErrorCodes.ProductNotFoundCode };

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(999, "products")).Returns(pdpItem);

            var result = this._productController.GetItem(999);
            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>)result;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("ProductItemDoesNotExist"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Product item was not found in the database."));
        }

        [Test]
        public void ReturnProductOrCollectionNameNotFoundErrorMsgIfProductIdIsInvalid()
        {
            var pdpItem = new PdpItem() { ProductName = ErrorCodes.ProductOrCollectionNotFoundCode };

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(999, "wrongCollection")).Returns(pdpItem);

            var result = this._productController.GetItem(999, "wrongCollection");
            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>)result;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("ProductItemOrCollectionNameDoesNotExist"));
            Assert.That(
                resultMessage.Content[0].ErrorMessage,
                Is.EqualTo("Product item or collection name was not found in the database."));
        }

        [Test]
        public void ReturnsNotNullObjectWhenRequestedByValidId()
        {
            var pdpItem = TestData.CreateTestPdpItem(123);

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(123, "products")).Returns(pdpItem);

            var result = this._productController.GetItem(123);

            var resultItem = ((OkNegotiatedContentResult<PdpItem>)result).Content;

            Assert.NotNull(resultItem);
        }

        [Test]
        public void ReturnsOkResponseIfProductIsFoundById()
        {
            var pdpItem = TestData.CreateTestPdpItem(123);

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(123, "products")).Returns(pdpItem);

            var result = this._productController.GetItem(123);

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<PdpItem>>());
        }
    }
}