using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductService;
using ProductService.Controllers;
using Moq;
using NUnit.Framework;
using ProductService.DataStore;
using ProductService.Models;
using Assert = NUnit.Framework.Assert;

namespace ProductService.Tests.Controllers
{
    [TestFixture]
    public class PlpControllerShould
    {
        private Mock<IProductsDataStore> _productAdapter;

        private PlpController _plpController;

        private Mock<ITelemetryLogger> _telemetryLogger;

        [SetUp]
        public void SetUp()
        {
            this._productAdapter = new Mock<IProductsDataStore>();
            this._plpController = new PlpController(this._productAdapter.Object);
            this._telemetryLogger = new Mock<ITelemetryLogger>();
        }

        [Test]
        public void Return_OK_response_for_GET_request()
        {
            var plpItems = new List<PlpItem> {CreateTestPlpItem(123), CreateTestPlpItem(345)};
            this._productAdapter.Setup(x => x.GetAllItemsAsync()).ReturnsAsync(plpItems);

            var result = this._plpController.GetItems().GetAwaiter().GetResult();

            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }

        [Test]
        public void Return_non_empty_list_of_products()
        {
            var plpItems = new List<PlpItem> { CreateTestPlpItem(123), CreateTestPlpItem(345) };

            this._productAdapter.Setup(x => x.GetAllItemsAsync()).ReturnsAsync(plpItems);

            var result = this._plpController.GetItems().GetAwaiter().GetResult();

            var resultItems = ((OkNegotiatedContentResult<List<PlpItem>>) result).Content;
            

            Assert.NotNull(resultItems);
        }

        [Test]
        public void Return_list_of_items()
        {
            var plpItems = new List<PlpItem> { CreateTestPlpItem(123), CreateTestPlpItem(345) };

            this._productAdapter.Setup(x => x.GetAllItemsAsync()).ReturnsAsync(plpItems);

            var result = this._plpController.GetItems().GetAwaiter().GetResult();

            var resultItems = ((OkNegotiatedContentResult<List<PlpItem>>) result).Content;
            
            Assert.That(resultItems, Is.EqualTo(plpItems));
        }

        [Test]
        public async Task Return_not_found_result_when_datastore_returns_no_products()
        {
            var plpItems = new List<PlpItem> {};

            this._productAdapter.Setup(x => x.GetAllItemsAsync()).ReturnsAsync(plpItems);

            var result = await this._plpController.GetItems();

            Assert.IsAssignableFrom<NotFoundResult>(result);

        }

            //Add test to check if data has been hard coded

        private static PlpItem CreateTestPlpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            return new PlpItem(id, productName, imageUrl, price, discountPrice);
        }
    }
}