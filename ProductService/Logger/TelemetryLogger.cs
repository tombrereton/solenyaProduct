namespace ProductService.Logger
{
    using System;
    using System.Collections.Generic;

    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;

    using ProductService.Models;

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

        public void Sanitize()
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset Timestamp { get; set; }

        public TelemetryContext Context { get; }

        public string Sequence { get; set; }
    }
}