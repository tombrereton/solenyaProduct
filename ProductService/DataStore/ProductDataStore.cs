using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductService.Models;

namespace ProductService.DataStore
{
    public class ProductDataStore : IProductsDataStore
    {
        private readonly string _dataPath;

        public ProductDataStore() : this("/../../../ProductService/DataStore/TestData.json")
        {
        }

        public ProductDataStore(string dataPath)
        {
            _dataPath = dataPath;
        }

        public async Task<IEnumerable<PlpItem>> GetAllItemsAsync()
        {
            return GetItems();
        }

        private static IEnumerable<PlpItem> GetItems()
        {
            const string productString = @"[
                            {   
                                ""productID"": 123,
                                ""productName"": ""Warehouse Side Split Roll Neck Jumper"",
                                ""splashImgUrl"": ""./products/product1/prod1-img1.jpg"",
                                ""price"": 4600,
                                ""discountPrice"": """"
                            },
                            { 
                                ""productID"": 234,
                                ""productName"": ""French Connection Checked Lined Harrington Jacket with Borg Collar"",
                                ""splashImgUrl"": ""./products/product2/prod2-img1.jpg"",
                                ""price"": 6000,
                                ""discountPrice"": """"
                            },
                            { 
                                ""productID"": 345,
                                ""productName"": ""Adidas Originals Trefoil Hoodie In Grey"",
                                ""splashImgUrl"": ""./products/product3/prod3-img1.jpg"",
                                ""price"": 5000,
                                ""discountPrice"": """"
                            },
                            {
                                ""productID"": 456,
                                ""productName"": ""ASOS Body With Plunge Neck Long Sleeve And Thong"",
                                ""splashImgUrl"": ""./products/product4/prod4-img1.jpg"",
                                ""price"": 1050,
                                ""discountPrice"": """"
                            },
                            { 
                                ""productID"": 567,
                                ""productName"": ""All Saints Oversized Zip Biker Jacket in Leather"",
                                ""splashImgUrl"": ""./products/product5/prod5-img1.jpg"",
                                ""price"": 42000,
                                ""discountPrice"": """"
                            }
                            ]";

            return JsonConvert.DeserializeObject<List<PlpItem>>(productString);
        }

        private List<PlpItem> LoadJson()

        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r =
                new StreamReader(currentDirectory + _dataPath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<PlpItem>>(json);
            }
        }
    }
}