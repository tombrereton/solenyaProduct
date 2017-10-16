namespace ProductService.Tests.Controllers
{
    internal interface ITelemetryLogger
    {
        void LogValidationErrors(IList<ValidationError> validationErrors);

        void LogException(Exception exception);
    }
}