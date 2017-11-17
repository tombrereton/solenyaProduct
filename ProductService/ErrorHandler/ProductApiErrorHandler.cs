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

        public ProductApiErrorHandler(ApiController controller, ITelemetryLogger telemetryLogger)
        {
            this._controller = controller;
            this._telemetryLogger = telemetryLogger;
        }
        public IHttpActionResult HandleFailedProductApiResponse(IEnumerable<ProductApiError> errors)
        {
            this._telemetryLogger.LogApiErrors(errors.ToList());
            return new NegotiatedContentResult<List<ProductApiError>>(HttpStatusCode.BadRequest, MapErrors(errors), this._controller);
        }

        private static List<ProductApiError> MapErrors(IEnumerable<ProductApiError> errors)
        {
            return errors.Select(error => new ProductApiError(error.ErrorCode, error.ErrorMessage)).ToList();
        }

        public static List<ProductApiError> Execute(PdpItem item)
        {
            var errors = new List<ProductApiError>();

            if (item == null)
            {
                var error = new ProductApiError("PdpItemDoesNotExist", "Pdp item was not found in the database.");
                errors.Add(error);
                return errors;
            }

            if (item.ProductId < 0)
            {
                var error = new ProductApiError("CollectionDoesNotExist", "Wrong collection was queried from database.");
                errors.Add(error);
            }

            return errors;
        }


    }
}