// <copyright file="PlpItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProductService.Models
{
    using System;
    using System.Collections.Generic;

    public class PdpItem
    {
        public PdpItem()
        {
        }

        public PdpItem(int productId, string productName, string splashImgUrl, int price, int? discountPrice, List<int> variants, Images imageOptions, string productDescription, string productBrand, string brandDescription, string materials, string gender)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.SplashImgUrl = splashImgUrl;
            this.Price = price;
            this.DiscountPrice = discountPrice;
            this.Variants = variants;
            this.ImageOptions = imageOptions;
            this.ProductDescription = ProductDescription;
            this.ProductBrand = productBrand;
            this.BrandDescription = brandDescription;
            this.Materials = materials;
            this.Gender = gender;
            
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string SplashImgUrl { get; set; }

        public int Price { get; set; }

        public int? DiscountPrice { get; set; }

        public List<int> Variants { get; set; }

        public Images ImageOptions { get; set; }

        public string ProductDescription { get; set; }

        public string ProductBrand { get; set; }

        public string BrandDescription { get; set; }

        public string Materials { get; set; }

        public string Gender { get; set; }


        public class Images
        {
            public Images()
            {
            }

            public Images(string colour, List<string> images)
            {
                this.Colour = colour;
                this.Images = images;
            }

            public string Colour { get; set; }

            public List<string> Images { get; set; }
        }

    }
}