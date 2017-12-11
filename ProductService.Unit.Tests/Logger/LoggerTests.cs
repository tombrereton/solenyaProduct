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