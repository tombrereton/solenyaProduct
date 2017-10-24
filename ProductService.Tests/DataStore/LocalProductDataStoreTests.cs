namespace ProductService.Tests.DataStore
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Models;

    [TestFixture]
    public class LocalProductDataStoreTests
    {
        private LocalProductDataStore _localProductDataStore;

        [SetUp]
        public void SetUp()
        {
            this._localProductDataStore = new LocalProductDataStore();
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = this._localProductDataStore.GetAllItemsAsync();

            var result = GetItems();

            CollectionAssert.AreEqual(itemsFromDataStore.Result.ToList(), result);
        }

        private static IEnumerable<PlpItem> GetItems()
        {
            const string ProductString = @"[
                            {   
                                ""productID"": 123,
                                ""productName"": ""Warehouse Side Split Roll Neck Jumper"",
                                ""splashImgUrl"": ""./static/media/prod1-img1.jpg"",
                                ""price"": 4600,
                                ""discountPrice"": """"
                            },
                            { 
                                ""productID"": 234,
                                ""productName"": ""French Connection Checked Lined Harrington Jacket with Borg Collar"",
                                ""splashImgUrl"": ""./static/media/prod2-img1.jpg"",
                                ""price"": 6000,
                                ""discountPrice"": """"
                            },
                            { 
                                ""productID"": 345,
                                ""productName"": ""Adidas Originals Trefoil Hoodie In Grey"",
                                ""splashImgUrl"": ""./static/media/prod3-img1.jpg"",
                                ""price"": 5000,
                                ""discountPrice"": """"
                            },
                            {
                                ""productID"": 456,
                                ""productName"": ""ASOS Body With Plunge Neck Long Sleeve And Thong"",
                                ""splashImgUrl"": ""./static/media/prod4-img1.jpg"",
                                ""price"": 1050,
                                ""discountPrice"": """"
                            },
                            { 
                                ""productID"": 567,
                                ""productName"": ""All Saints Oversized Zip Biker Jacket in Leather"",
                                ""splashImgUrl"": ""./static/media/prod5-img1.jpg"",
                                ""price"": 42000,
                                ""discountPrice"": """"
                            }
                            ]";

            return JsonConvert.DeserializeObject<List<PlpItem>>(ProductString);
        }
}
}