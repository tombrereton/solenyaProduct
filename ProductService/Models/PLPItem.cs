using System;
using System.Drawing;

namespace ProductService.Models
{
    public class PlpItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SpashImgUrl { get; set; }
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }

        public PlpItem()
        {
            
        }

        public PlpItem(int productId, string productName, string spashImgUrl, int price, int? discountPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.SpashImgUrl = spashImgUrl;
            this.Price = price;
            this.DiscountPrice = discountPrice;
        }

        public override bool Equals(Object obj)
        {
            PlpItem plpItem = (PlpItem) obj;
            // Check for null values and compare run-time types.
            if (plpItem == null || GetType() != plpItem.GetType())
                return false;

            if (this.ProductId != plpItem.ProductId ||
                this.ProductName != plpItem.ProductName ||
                this.SpashImgUrl != plpItem.SpashImgUrl ||
                this.Price != plpItem.Price ||
                this.DiscountPrice != plpItem.DiscountPrice)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}