// <copyright file="PlpItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProductService.Models
{
    using System;
    using System.Collections.Generic;

    public class PlpItem
    {
        public PlpItem()
        {
        }

        public PlpItem(int productId, string productName, string splashImgUrl, int price, int? discountPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.SplashImgUrl = splashImgUrl;
            this.Price = price;
            this.DiscountPrice = discountPrice;
        }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public string SplashImgUrl { get; set; }

        public int? Price { get; set; }

        public int? DiscountPrice { get; set; }

        public override bool Equals(object obj)
        {
            var plpItem = (PlpItem)obj;

            // Check for null values and compare run-time types.
            if (plpItem == null || this.GetType() != plpItem.GetType())
            {
                return false;
            }

            return this.ProductId == plpItem.ProductId && this.ProductName == plpItem.ProductName
                   && this.SplashImgUrl == plpItem.SplashImgUrl && this.Price == plpItem.Price
                   && this.DiscountPrice == plpItem.DiscountPrice;
        }

        public override int GetHashCode()
        {
            var hashCode = -472697689;
            hashCode = (hashCode * -1521134295) + this.ProductId.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.ProductName);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.SplashImgUrl);
            hashCode = (hashCode * -1521134295) + this.Price.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<int?>.Default.GetHashCode(this.DiscountPrice);
            return hashCode;
        }

        public override string ToString()
        {
            return string.Empty + this.ProductId + this.ProductName + this.Price + this.DiscountPrice
                   + this.SplashImgUrl;
        }
    }
}