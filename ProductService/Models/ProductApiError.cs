namespace ProductService.Models
{
    public class ProductApiError
    {
        public ProductApiError(string errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}