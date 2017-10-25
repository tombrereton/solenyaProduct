namespace ProductService.Tests.DataStore
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Models;

    [TestFixture]
    public class ProductDataStoreTests
    {
        private ProductDataStore _productDataStore;

        [SetUp]
        public void SetUp()
        {
            this._productDataStore = new ProductDataStore();
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = _productDataStore.GetAllItemsAsync();

            var expectedData = GetItems();

            CollectionAssert.AreEqual(itemsFromDataStore.Result.ToList(), expectedData);
        }

        private static IEnumerable<PlpItem> GetItems()
        {
            const string ProductString = @"[
                            {   
                                ""ProductId"": 1,
                                ""ProductName"": ""Warehouse Side Split Roll Neck Jumper"",
                                ""SplashImgUrl"": ""./static/media/prod1-img1.jpg"",
                                ""Price"": 4600,
                                ""DiscountPrice"": """"
                            },
                            { 
                                ""ProductId"": 2,
                                ""ProductName"": ""French Connection Checked Lined Harrington Jacket with Borg Collar"",
                                ""SplashImgUrl"": ""./static/media/prod2-img1.jpg"",
                                ""Price"": 6000,
                                ""DiscountPrice"": """"
                            },
                            { 
                                ""ProductId"": 3,
                                ""ProductName"": ""Adidas Originals Trefoil Hoodie In Grey"",
                                ""SplashImgUrl"": ""./static/media/prod3-img1.jpg"",
                                ""Price"": 5000,
                                ""DiscountPrice"": """"
                            },
                            {
                                ""ProductId"": 4,
                                ""ProductName"": ""ASOS Body With Plunge Neck Long Sleeve And Thong"",
                                ""SplashImgUrl"": ""./static/media/prod4-img1.jpg"",
                                ""Price"": 1050,
                                ""DiscountPrice"": """"
                            },
                            { 
                                ""ProductId"": 5,
                                ""ProductName"": ""All Saints Oversized Zip Biker Jacket in Leather"",
                                ""SplashImgUrl"": ""./static/media/prod5-img1.jpg"",
                                ""Price"": 42000,
                                ""DiscountPrice"": """"
                            }
                            ]";

            return JsonConvert.DeserializeObject<List<PlpItem>>(ProductString);
        }
}
}