namespace ProductService.Models
{
    public class PlpItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }

        public PlpItem(int productId, string productName, string imageUrl, int price, int? discountPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.ImageUrl = imageUrl;
            this.Price = price;
            this.DiscountPrice = discountPrice;
        }

    }
}