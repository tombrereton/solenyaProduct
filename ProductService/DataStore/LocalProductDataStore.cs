namespace ProductService.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.Documents.SystemFunctions;

    using Newtonsoft.Json;

    using ProductService.Models;

    /// <summary>
    /// The product data store.
    /// </summary>
    public class LocalProductDataStore : IProductsDataStore
    {
        /// <summary>
        /// The get all items async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public IEnumerable<PlpItem> GetAllPlpItemsFromCollection(string collectionName)
        {
            return GetItems();
        }

        public PdpItem GetPdpItemFromCollection(int id, string collectionName)
        {
            return GetItem(id);
        }

        public Task RemoveDocumentCollection(string collectionName)
        {
            return null;
        }

        public Task CreateDocumentCollection(string collectionName)
        {
            return null;
        }

        public Task CreatePdpDocumentIfNotExists(string collectionName, PdpItem pdpItem)
        {
            return null;
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
                                ""price"": 1800,
                                ""discountPrice"": 1050
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

        private PdpItem GetItem(int id)
        {
            const string ProductString = @"[
                            {   
                                ""productID"": 123,
                                ""productName"": ""Warehouse Side Split Roll Neck Jumper"",
                                ""splashImgUrl"": ""./static/media/prod1-img1.jpg"",
                                ""price"": 4600,
                                ""discountPrice"": """",
                                ""variants"": [
                                            {
                                                ""variantId"": 1
                                            },
                                            {
                                                ""variantId"": 2
                                            },
                                            {
                                                ""variantId"": 3
                                            },
                                            {
                                                ""variantId"": 4
                                            },
                                            {
                                                ""variantId"": 5
                                            },
                                            {
                                                ""variantId"": 6
                                            }
                                            ],
                                            ""imageOptions"": [
                                            {
                                                ""colour"": ""Olive marl"",
                                                ""imageList"": [
                                                ""./static/media/prod1-img1.jpg"",
                                                ""./static/media/prod1-img2.jpg"",
                                                ""./static/media/prod1-img3.jpg"",
                                                ""./static/media/prod1-img4.jpg""
                                                    ]
                                            }
                                            ],
                                            ""productDescription"": ""Jumper by Warehouse, Textured knit, Cowl neck, Raglan sleeves, Ribbed trims, Split hem, Regular fit - true to size"",
                                            ""productBrand"": ""Warehouse"",
                                            ""brandDescription"": ""Delivering seasonal trends for the high street, Warehouse offer a collection of directional pieces, with vibrant prints and clean cut tailoring channelling the brand's signature style. Statement party dresses sit alongside classic wardrobe staples, all complemented by a key range of jewellery and accessories, including an exclusive edit of three satchel bags."",
                                            ""materials"": ""55% Polyamide, 40% Acrylic, 5% Wool"",
                                            ""gender"": ""Women"",
                                
                            }
                        ]";

            return JsonConvert.DeserializeObject<PdpItem>(ProductString);
        }
    }
}