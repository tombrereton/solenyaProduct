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
        private Mock<IProductsDataStore> _dataStore;

        private ProductController _productController;

        private Mock<ITelemetryLogger> _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._dataStore = new Mock<IProductsDataStore>();
            this._productController = new ProductController(this._dataStore.Object);
            this._telemetryLogger = new Mock<ITelemetryLogger>();
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
        public async Task ReturnNotFoundResultWhenDatastoreReturnsNoProductsForPlp()
        {
            this._dataStore.Setup(x => x.GetAllPlpItemsFromCollection("test_data_product"))
                .Returns(new List<PlpItem>());

            var result = this._productController.GetItems();

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        // Add test to check if data has been hard coded
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
        public void ReturnNotFoundResponseIfProductNotFound()
        {
            this._dataStore.Setup(x => x.GetPdpItemFromCollection(123, "test_data_product")).Returns(new PdpItem());

            var result = this._productController.GetItem(123);

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Test]
        public void ReturnErrorIfProductIdIsInvalid()
        {
            var pdpItem = TestData.CreateTestPdpItem(123);

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(123, "products")).Returns(pdpItem);

            var result = this._productController.GetItem(999);
            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<ProductApiError>>>());
            var resultMessage = (OkNegotiatedContentResult<List<ProductApiError>>)result;

            Assert.That(resultMessage.Content[0].ErrorCode, Is.EqualTo("PdpItemDoesNotExist"));
            Assert.That(resultMessage.Content[0].ErrorMessage, Is.EqualTo("Pdp item was not found in the database."));
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

        [Test]
        public void LogNotFoundErrorWhenNoPdpItemFound()
        {
            var pdpItem = TestData.CreateTestPdpItem(123);

            this._dataStore.Setup(x => x.GetPdpItemFromCollection(123, "products")).Returns(pdpItem);

            var result = this._productController.GetItem(999);

            this._telemetryLogger.Verify(
                x => x.LogApiErrors(
                    It.Is<List<ProductApiError>>(
                        errors => AssertProductApiError(
                            errors,
                            "PdpItemDoesNotExist",
                            "Pdp item was not found in the database."))),
                Times.Once);
        }

        private static bool AssertProductApiError(
            List<ProductApiError> validationError,
            string expectedErrorCode,
            string expectedErrorMessage)
        {
            Assert.That(validationError[0].ErrorCode, Is.EqualTo(expectedErrorCode));
            Assert.That(validationError[0].ErrorMessage, Is.EqualTo(expectedErrorMessage));

            return true;
        }
    }
}