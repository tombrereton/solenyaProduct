namespace ProductService.Models
{
    public class ValidationError
    {
        public ValidationError(string errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}