using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductService.Telemetry
{
    using Microsoft.ApplicationInsights;

    using ProductService.Models;
    using ProductService.Tests.Controllers;

    public class TelemetryLogger : ITelemetryLogger
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetryLogger(TelemetryClient telemetryClient)
        {
            this._telemetryClient = telemetryClient;
        }

        public void LogApiErrors(IList<ProductApiError> productApiErrors)
        {
            if (productApiErrors == null)
            {
                throw new ArgumentNullException(nameof(productApiErrors));
            }

            foreach (var productApiError in productApiErrors)
            {
                var eventProperties = new Dictionary<string, string>
                                          {
                                              { nameof(ProductApiError.ErrorCode), productApiError.ErrorCode },
                                              { nameof(ProductApiError.ErrorMessage), productApiError.ErrorMessage }
                                          };

                this._telemetryClient.TrackEvent("ProductApiFailure", eventProperties);
            }
        }

        public void LogException(Exception exception)
        {
            this._telemetryClient.TrackException(exception);
        }
    }
}