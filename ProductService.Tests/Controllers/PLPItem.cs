namespace ProductService.Tests.Controllers
{
    public class PLPItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageURL { get; set; }
        public int Price { get; set; }
        public int? DiscountPrice { get; set; }

        public PLPItem(int productId, string productName, string imageUrl, int price, int? discountPrice)
        {
            this.ProductID = productId;
            ProductName = productName;
            ImageURL = imageUrl;
            Price = price;
            DiscountPrice = discountPrice;
        }
    }
}