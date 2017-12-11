namespace ProductService.Integration.Tests.TestData
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using ProductService.DataStore;
    using ProductService.Models;

    public class TestData
    {
        public static void TearDownDBTestData(IProductsDataStore productDataStore, string collectionName)
        {
            productDataStore.RemoveDocumentCollection(collectionName).Wait();
        }

        public static void SetUpDBWithTestData(IProductsDataStore productDataStore, string collectionName)
        {
            productDataStore.CreateDocumentCollection(collectionName).Wait();

            var testDataItems = GeneratePdpItemTestData();
            foreach (PdpItem testDataItem in testDataItems)
            {
                productDataStore.CreatePdpDocumentIfNotExists(collectionName, testDataItem).Wait();
            }
        }

        public static List<PdpItem> GeneratePdpItemTestData()
        {
            var pdpItem1 = CreateTestPdpItem(123);
            var pdpItem2 = CreateTestPdpItem(234);
            var pdpItem3 = CreateTestPdpItem(345);

            var pdpItems = new List<PdpItem> { pdpItem1, pdpItem2, pdpItem3 };

            return pdpItems;
        }

        public static List<PlpItem> GeneratePlpItemTestData()
        {
            var plpItem1 = CreateTestPlpItem(123);
            var plpItem2 = CreateTestPlpItem(234);
            var plpItem3 = CreateTestPlpItem(345);

            var plpItems = new List<PlpItem> { plpItem1, plpItem2, plpItem3 };

            return plpItems;
        }

        public static PdpItem GenerateSinglePdpItemTestData()
        {
            return CreateTestPdpItem(123);
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

        public static PlpItem CreateTestPlpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            return new PlpItem(id, productName, imageUrl, price, discountPrice);
        }

        public static PdpItem CreateTestPdpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            PdpItem.Variant[] variants = new[] { new PdpItem.Variant(1) };
            PdpItem.Image[] imageOptions = new[] { new PdpItem.Image("red", new[] { "a", "b", "c" }) };
            string productDescription = "Test Description";
            string productBrand = "Test Brand Name";
            string brandDescription = "Test Brand Description";
            string materials = "Test Materials";
            string gender = "Test Gender";

            return new PdpItem(
                id,
                productName,
                imageUrl,
                price,
                discountPrice,
                variants,
                imageOptions,
                productDescription,
                productBrand,
                brandDescription,
                materials,
                gender);
        }
    }
}