// <copyright file="PlpItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProductService.Models
{
    public class PdpItem
    {
        public PdpItem()
        {
        }

        public PdpItem(
            int productId,
            string productName,
            string splashImgUrl,
            int price,
            int? discountPrice,
            Variant[] variants,
            Image[] imageOptions,
            string productDescription,
            string productBrand,
            string brandDescription,
            string materials,
            string gender)
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

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public string SplashImgUrl { get; set; }

        public int? Price { get; set; }

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

            public Variant()
            {
            }

            public Variant(int id)
            {
                this.VariantId = id;
            }

            public override string ToString()
            {
                return string.Empty + this.VariantId;
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

            public override string ToString()
            {
                return string.Empty + this.Colour + this.ImageList;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(PdpItem))
            {
                return false;
            }

            return this.ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Empty + this.ProductId + this.ProductName + this.Price + this.DiscountPrice
                   + this.BrandDescription + this.ProductBrand + this.ProductDescription + this.Gender + this.Materials
                   + this.SplashImgUrl + this.ImageOptions + this.Variants;
        }
    }
}