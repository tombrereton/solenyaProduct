using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http.Results;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.DataStore;
using ProductService.Models;
using ProductService.Tests.Controllers;

namespace ProductService.Tests.DataStore
{
    [TestFixture]
    public class ProductDataStoreTest
    {
        private ProductDataStore _productDataStore;

        [SetUp]
        public void SetUp()
        {
            this._productDataStore = new ProductDataStore();
        }

        [Test]
        public void Return_exact_list_of_items()
        {
            List<PlpItem> itemsFromDataStore = (List<PlpItem>) this._productDataStore.GetAllItemsAsync().Result;

            // import items from json file and assign to variable
            var result = LoadJson();

            CollectionAssert.AreEqual(itemsFromDataStore, result);
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
            using (StreamReader r = new StreamReader(currentDirectory + "/../../../ProductService/DataStore/TestData.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<PlpItem>>(json);
            }
        }

}
}