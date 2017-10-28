namespace ProductService.Tests.TestData
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ProductService.DataStore;
    using ProductService.Models;

    public class TestData
    {
        public static async Task TearDownDBTestData(IProductsDataStore productDataStore)
        {
            string testDataCollection = "test_data_product";
            await productDataStore.RemoveDocumentCollection(testDataCollection).ConfigureAwait(false);
        }

        public static async Task SetUpDBWithTestData(IProductsDataStore productDataStore)
        {
            string testDataCollection = "test_data_product";
            await productDataStore.CreateDocumentCollection(testDataCollection).ConfigureAwait(false);

            var testDataItems = GeneratePdpItemTestData();
            foreach (PdpItem testDataItem in testDataItems)
            {
                await productDataStore.CreatePdpDocumentIfNotExists(testDataCollection, testDataItem)
                    .ConfigureAwait(false);
            }
        }

        public static List<PdpItem> GeneratePdpItemTestData()
        {
            // TODO: create a list of PdpItems
            return new List<PdpItem>();
        }

        public static IEnumerable<PlpItem> GetDBItems()
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
                                ""Price"": 1800,
                                ""DiscountPrice"": 1050
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

        public static IEnumerable<PlpItem> GetLocalItems()
        {
            const string ProductString = @"[
                            {   
                                ""ProductId"": 123,
                                ""ProductName"": ""Warehouse Side Split Roll Neck Jumper"",
                                ""SplashImgUrl"": ""./static/media/prod1-img1.jpg"",
                                ""Price"": 4600,
                                ""DiscountPrice"": """"
                            },
                            { 
                                ""ProductId"": 234,
                                ""ProductName"": ""French Connection Checked Lined Harrington Jacket with Borg Collar"",
                                ""SplashImgUrl"": ""./static/media/prod2-img1.jpg"",
                                ""Price"": 6000,
                                ""DiscountPrice"": """"
                            },
                            { 
                                ""ProductId"": 345,
                                ""ProductName"": ""Adidas Originals Trefoil Hoodie In Grey"",
                                ""SplashImgUrl"": ""./static/media/prod3-img1.jpg"",
                                ""Price"": 5000,
                                ""DiscountPrice"": """"
                            },
                            {
                                ""ProductId"": 456,
                                ""ProductName"": ""ASOS Body With Plunge Neck Long Sleeve And Thong"",
                                ""SplashImgUrl"": ""./static/media/prod4-img1.jpg"",
                                ""Price"": 1800,
                                ""DiscountPrice"": 1050
                            },
                            { 
                                ""ProductId"": 567,
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