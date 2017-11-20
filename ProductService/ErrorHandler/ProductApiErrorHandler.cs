namespace ProductService.ErrorHandler
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;

    using ProductService.Models;
    using ProductService.Tests.Controllers;

    public class ProductApiErrorHandler
    {
        private readonly ApiController _controller;

        private readonly ITelemetryLogger _telemetryLogger;

        public static List<ProductApiError> Execute(PdpItem item)
        {
            var errors = new List<ProductApiError>();

            if (item.ProductName.Equals(ErrorCodes.ProductNotFoundCode))
            {
                var error = new ProductApiError(
                    ErrorCodes.ProductNotFoundCode,
                    ErrorCodes.ProductNotFoundInDatabaseMsg);
                errors.Add(error);
                return errors;
            }
            else if (item.ProductName.Equals(ErrorCodes.ProductOrCollectionNotFoundCode))
            {
                var error = new ProductApiError(
                    ErrorCodes.ProductOrCollectionNotFoundCode,
                    ErrorCodes.ProductOrCollectionNotFoundMsg);
                errors.Add(error);
            }

            return errors;
        }

        public static List<ProductApiError> Execute(List<PlpItem> items)
        {
            var errors = new List<ProductApiError>();

            if (items[0].ProductName.Equals(ErrorCodes.CollectionNotFoundCode))
            {
                var error = new ProductApiError(ErrorCodes.CollectionNotFoundCode, ErrorCodes.CollectionNotFoundMsg);
                errors.Add(error);
                return errors;
            }

            return errors;
        }
    }
}