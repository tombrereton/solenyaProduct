using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http.Results;
using Newtonsoft.Json;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.DataStore;
using ProductService.Models;

namespace ProductService.Tests.Integration
{
    [TestFixture]
    public class IntegrationControllerDataStoreTest
    {
        private ProductDataStore _productDataStore;
        private PlpController _controller;

        [SetUp]
        public void SetUp()
        {
            this._productDataStore = new ProductDataStore();
            this._controller = new PlpController(this._productDataStore);
        }

        [Test]
        public void Should_return_contents_matching_list_from_json_with_controller_and_datastore()
        {
            var response = this._controller.GetItems().GetAwaiter().GetResult();
            var response_contents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = LoadJson();

            CollectionAssert.AreEqual(response_contents, result);
        }

        [Test]
        public void Should_return_all_items_with_controller_and_datastore()
        {
            var response = this._controller.GetItems().GetAwaiter().GetResult();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }


        private static PlpItem CreateTestPlpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            return new PlpItem(id, productName, imageUrl, price, discountPrice);
        }

        private List<PlpItem> LoadJson()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r =
                new StreamReader(currentDirectory + "/../../../ProductService/DataStore/TestData.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<PlpItem>>(json);
            }
        }
    }
}