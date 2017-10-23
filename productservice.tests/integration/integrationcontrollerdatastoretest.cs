﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http.Results;
using Newtonsoft.Json;
using NUnit.Framework;
using ProductService.Controllers;
using ProductService.DataStore;
using ProductService.Models;

namespace ProductService.Tests.Integration
{
    [TestFixture]
    public class IntegrationControllerDataStoreTest
    {
        private ProductDataStore _productDataStore;
        private ProductController _controller;

        [SetUp]
        public void SetUp()
        {
            this._productDataStore = new ProductDataStore();
            this._controller = new ProductController(this._productDataStore);
        }

        [Test]
        public void Should_return_contents_matching_list_from_json_with_controller_and_datastore()
        {
            var response = this._controller.GetItems().GetAwaiter().GetResult();
            var response_contents = ((OkNegotiatedContentResult<List<PlpItem>>)response).Content;

            // import items from json file and assign to variable
            var result = GetItems();

            CollectionAssert.AreEqual(response_contents, result);
        }

        [Test]
        public void Should_return_all_items_with_controller_and_datastore()
        {
            var response = this._controller.GetItems().GetAwaiter().GetResult();

            Assert.That(response, Is.InstanceOf<OkNegotiatedContentResult<List<PlpItem>>>());
        }


        private static PlpItem CreateTestPlpItem(int id)
        {
            string productName = "Test Product";
            string imageUrl = "Test URL";
            int price = 2000;
            int discountPrice = 1500;
            return new PlpItem(id, productName, imageUrl, price, discountPrice);
        }

        private static IEnumerable<PlpItem> GetItems()
        {
            const string productString = @"[
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

            return JsonConvert.DeserializeObject<List<PlpItem>>(productString);
        }

    }
}