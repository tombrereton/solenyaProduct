using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
//            this._productAdapter = new Mock<IProductsDataStore>();
//            this._plpController = new PlpController(this._productAdapter.Object);
//            this._telemetryLogger = new Mock<ITelemetryLogger>();
        }

        [Test]
        public void Return_exact_list_of_items()
        {
            // call data store method to return all items and store in variable 
            var itemsFromDataStore = this._productDataStore.GetAllItemsAsync();

            // import items from json file and assign to variable
            

            // assert the 2 above are the same
        }


        private static PlpItem CreateTestPlpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            return new PlpItem(id, productName, imageUrl, price, discountPrice);
        }
        private void LoadJson()
        {
            using (StreamReader r = new StreamReader("../../ProductService/DataStore/TestData.json"))
            {
                string json = r.ReadToEnd();
                List<PlpItem> items = JsonConvert.DeserializeObject<List<PlpItem>>(json);
            }
        }

}

    internal class ProductDataStore : IProductsDataStore
    {
        public Task<IEnumerable<PlpItem>> GetAllItemsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}