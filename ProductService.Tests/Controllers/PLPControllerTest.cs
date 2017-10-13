using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductService;
using ProductService.Controllers;
using Moq;
using NUnit.Framework;

namespace ProductService.Tests.Controllers
{
    [TestFixture]
    public class PLPControllerTest
    {
        private Mock<IProductsDataStore> productAdapter;

        private PLPController plpController;

        private Mock<ITelemetryLogger> telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this.productAdapter = new Mock<IProductsDataStore>();
            this.plpController = new PLPController(this.productAdapter.Object);
            this.telemetryLogger = new Mock<ITelemetryLogger>();
        }


    }
}