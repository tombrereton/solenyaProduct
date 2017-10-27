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

        public PdpItem(int productId, string productName, string splashImgUrl, int price, int? discountPrice, Variant[] variants, Image[] imageOptions, string productDescription, string productBrand, string brandDescription, string materials, string gender)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.SplashImgUrl = splashImgUrl;
            this.Price = price;
            this.DiscountPrice = discountPrice;
            this.Variants = variants;
            this.ImageOptions = imageOptions;
            this.ProductDescription = productDescription;
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

        public Variant[] Variants { get; set; }

        public Image[] ImageOptions { get; set; }

        public string ProductDescription { get; set; }

        public string ProductBrand { get; set; }

        public string BrandDescription { get; set; }

        public string Materials { get; set; }

        public string Gender { get; set; }


        public class Variant
        {
            public int VariantId { get; set; }

            public Variant(int id)
            {
                this.VariantId = id;
            }
        }

        public class Image
        {
            public Image()
            {
            }

            public Image(string colour, string[] imageList)
            {
                this.Colour = colour;
                this.ImageList = imageList;
            }

            public string Colour { get; set; }

            public string[] ImageList { get; set; }
        }

        public static List<PdpItem> GeneratePdpDbData()
        {
            List<PdpItem> pdpCollection = new List<PdpItem>();

            PdpItem testPdpItem = new PdpItem
            {
                ProductId = 1,
                ProductName = "Warehouse Side Split Roll Neck Jumper",
                SplashImgUrl = "./products/product1/prod1-img1.jpg",
                Price = 4600,
                DiscountPrice = null,
                Variants = new PdpItem.Variant[]
                                                    {
                                                         new PdpItem.Variant
                                                             {
                                                                 VariantId = 1,
                                                             }
                                                    },
                ImageOptions = new PdpItem.Image[]
                                                        {
                                                            new PdpItem.Image
                                                                {
                                                                    Colour = "Olive marl",
                                                                    ImageList = new string[]
                                                                                    {
                                                                                        "./products/product1/prod1-img1.jpg",
                                                                                        "./products/product1/prod1-img2.jpg",
                                                                                        "./products/product1/prod1-img3.jpg",
                                                                                        "./products/product1/prod1-img4.jpg",
                                                                                    }
                                                                }
                                                        },
                ProductDescription = "Jumper by Warehouse, Textured knit, Cowl neck, Raglan sleeves, Ribbed trims, Split hem, Regular fit - true to size",
                ProductBrand = "Warehouse",
                BrandDescription = "Delivering seasonal trends for the high street, Warehouse offer a collection of directional pieces, with vibrant prints and clean cut tailoring channelling the brand's signature style. Statement party dresses sit alongside classic wardrobe staples, all complemented by a key range of jewellery and accessories, including an exclusive edit of three satchel bags.",
                Materials = "55% Polyamide, 40% Acrylic, 5% Wool",
                Gender = "Women",

            };

            pdpCollection.Add(testPdpItem);

            return pdpCollection;
        }

    }
}