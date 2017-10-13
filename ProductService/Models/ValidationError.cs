namespace ProductService.Tests.Controllers
{
    public class ValidationError
    {
        public ValidationError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; }

        public string ErrorMessage { get; }
    }
}