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

        private static PdpItem CreateTestPdpItem(int id)
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

        public static List<PdpItem> GeneratePdpDbData()
        {
            List<PdpItem> pdpCollection = new List<PdpItem>();

            PdpItem testPdpItem1 = new PdpItem
                                       {
                                           ProductId = 1,
                                           ProductName = "Warehouse Side Split Roll Neck Jumper",
                                           SplashImgUrl = "./static/media/prod1-img1.jpg",
                                           Price = 4600,
                                           DiscountPrice = null,
                                           Variants = new[] { new PdpItem.Variant { VariantId = 1 } },
                                           ImageOptions =
                                               new[]
                                                   {
                                                       new PdpItem.Image
                                                           {
                                                               Colour = "Olive marl",
                                                               ImageList =
                                                                   new[]
                                                                       {
                                                                           "./products/product1/prod1-img1.jpg",
                                                                           "./products/product1/prod1-img2.jpg",
                                                                           "./products/product1/prod1-img3.jpg",
                                                                           "./products/product1/prod1-img4.jpg",
                                                                       }
                                                           }
                                                   },
                                           ProductDescription =
                                               "Jumper by Warehouse, Textured knit, Cowl neck, Raglan sleeves, Ribbed trims, Split hem, Regular fit - true to size",
                                           ProductBrand = "Warehouse",
                                           BrandDescription =
                                               "Delivering seasonal trends for the high street, Warehouse offer a collection of directional pieces, with vibrant prints and clean cut tailoring channelling the brand's signature style. Statement party dresses sit alongside classic wardrobe staples, all complemented by a key range of jewellery and accessories, including an exclusive edit of three satchel bags.",
                                           Materials = "55% Polyamide, 40% Acrylic, 5% Wool",
                                           Gender = "Women",
                                       };

            PdpItem testPdpItem2 = new PdpItem
                                       {
                                           ProductId = 2,
                                           ProductName = "Warehouse Side Split Roll Neck Jumper",
                                           SplashImgUrl = "./static/media/prod2-img1.jpg",
                                           Price = 6000,
                                           DiscountPrice = null,
                                           Variants = new[] { new PdpItem.Variant { VariantId = 2 } },
                                           ImageOptions =
                                               new[]
                                                   {
                                                       new PdpItem.Image
                                                           {
                                                               Colour = "Olive marl",
                                                               ImageList =
                                                                   new[]
                                                                       {
                                                                           "./products/product2/prod1-img1.jpg",
                                                                           "./products/product2/prod1-img2.jpg",
                                                                           "./products/product2/prod1-img3.jpg",
                                                                           "./products/product2/prod1-img4.jpg",
                                                                       }
                                                           }
                                                   },
                                           ProductDescription =
                                               "Jumper by Warehouse, Textured knit, Cowl neck, Raglan sleeves, Ribbed trims, Split hem, Regular fit - true to size",
                                           ProductBrand = "Warehouse",
                                           BrandDescription =
                                               "Delivering seasonal trends for the high street, Warehouse offer a collection of directional pieces, with vibrant prints and clean cut tailoring channelling the brand's signature style. Statement party dresses sit alongside classic wardrobe staples, all complemented by a key range of jewellery and accessories, including an exclusive edit of three satchel bags.",
                                           Materials = "55% Polyamide, 40% Acrylic, 5% Wool",
                                           Gender = "Women",
                                       };

            PdpItem testPdpItem3 = new PdpItem
                                       {
                                           ProductId = 3,
                                           ProductName = "Adidas Originals Trefoil Hoodie In Grey",
                                           SplashImgUrl = "./static/media/prod3-img1.jpg",
                                           Price = 5000,
                                           DiscountPrice = null,
                                           Variants = new[] { new PdpItem.Variant { VariantId = 3 } },
                                           ImageOptions =
                                               new[]
                                                   {
                                                       new PdpItem.Image
                                                           {
                                                               Colour = "Olive marl",
                                                               ImageList =
                                                                   new[]
                                                                       {
                                                                           "./products/product3/prod1-img1.jpg",
                                                                           "./products/product3/prod1-img2.jpg",
                                                                           "./products/product3/prod1-img3.jpg",
                                                                           "./products/product3/prod1-img4.jpg",
                                                                       }
                                                           }
                                                   },
                                           ProductDescription =
                                               "Jumper by Warehouse, Textured knit, Cowl neck, Raglan sleeves, Ribbed trims, Split hem, Regular fit - true to size",
                                           ProductBrand = "Warehouse",
                                           BrandDescription =
                                               "Delivering seasonal trends for the high street, Warehouse offer a collection of directional pieces, with vibrant prints and clean cut tailoring channelling the brand's signature style. Statement party dresses sit alongside classic wardrobe staples, all complemented by a key range of jewellery and accessories, including an exclusive edit of three satchel bags.",
                                           Materials = "55% Polyamide, 40% Acrylic, 5% Wool",
                                           Gender = "Women",
                                       };

            pdpCollection.Add(testPdpItem1);
            pdpCollection.Add(testPdpItem2);
            pdpCollection.Add(testPdpItem2);

            return pdpCollection;
        }
    }
}