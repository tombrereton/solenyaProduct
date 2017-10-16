namespace ProductService.Models
{
    public class PlpItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SpashImgUrl { get; set; }
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }

        public PlpItem(int productId, string productName, string spashImgUrl, int price, int? discountPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.SpashImgUrl = spashImgUrl;
            this.Price = price;
            this.DiscountPrice = discountPrice;
        }

    }
}