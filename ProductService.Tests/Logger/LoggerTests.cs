namespace ProductService.Tests.Controllers
{
    using System.Collections.Generic;

    using Microsoft.ApplicationInsights;

    using NUnit.Framework;

    using ProductService.ErrorHandler;
    using ProductService.Logger;
    using ProductService.Models;

    using Assert = NUnit.Framework.Assert;

    [TestFixture]
    public class LoggerTests
    {
        private ITelemetryLogger _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._telemetryLogger = new TelemetryLogger(new TelemetryClient());
        }

        [Test]
        public void LogProductNotFoundErrorWhenNoPdpItemFound()
        {
            // this._telemetryLogger.Verify(
            // x => x.LogApiErrors(
            // It.Is<List<ProductApiError>>(
            // errors => AssertProductApiError(
            // errors,
            // "ProductItemDoesNotExist",
            // "Product item was not found in the database."))),
            // Times.Once);
        }

        [Test]
        public void LogProductOrCollectionNotFoundErrorWhenNoPdpItemFound()
        {
            var pdpItem = new PdpItem() { ProductName = ErrorCodes.ProductOrCollectionNotFoundCode };

            // this._telemetryLogger.Verify(
            // x => x.LogApiErrors(
            // It.Is<List<ProductApiError>>(
            // errors => AssertProductApiError(
            // errors,
            // "ProductItemOrCollectionNameDoesNotExist",
            // "Product item or collection name was not found in the database."))),
            // Times.Once);
        }

        [Test]
        public void LogCollectionNotFoundErrorWhenNoPlpItemsFound()
        {
            var items = new List<PlpItem>() { new PlpItem() { ProductName = ErrorCodes.CollectionNotFoundCode } };

            // this._telemetryLogger.Verify(
            // x => x.LogApiErrors(
            // It.Is<List<ProductApiError>>(
            // errors => AssertProductApiError(
            // errors,
            // "CollectionNameDoesNotExist",
            // "Collection name was not found in the database."))),
            // Times.Once);
        }

        private static bool AssertProductApiError(
            List<ProductApiError> productApiErrors,
            string expectedErrorCode,
            string expectedErrorMessage)
        {
            Assert.That(productApiErrors[0].ErrorCode, Is.EqualTo(expectedErrorCode));
            Assert.That(productApiErrors[0].ErrorMessage, Is.EqualTo(expectedErrorMessage));

            return true;
        }
    }
}