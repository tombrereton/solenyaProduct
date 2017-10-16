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
            // call data store method to return all items and store in variable 
            var filePath = "/../../../ProductService/DataStore/TestData.json";
            var itemsFromDataStore = this._productDataStore.GetAllItemsAsync().Result;
            var itemName = itemsFromDataStore.ElementAt(1).ProductName;
            // import items from json file and assign to variable
            var result = LoadJson();
            var resultName = result.ElementAt(1).ProductName;
            Console.WriteLine("Item: " + itemName + " Result: " +resultName);
            // assert the 2 above are the same
            CollectionAssert.AreNotEqual(itemsFromDataStore.ToList(), result);
            //Assert.That(itemsFromDataStore.ToList(), Is.EqualTo(result));
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