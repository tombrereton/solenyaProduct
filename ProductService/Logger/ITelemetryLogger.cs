namespace ProductService.Logger
{
    using System;
    using System.Collections.Generic;

    using Microsoft.ApplicationInsights.Channel;

    using ProductService.Models;

    public interface ITelemetryLogger : ITelemetry
    {
        void LogApiErrors(IList<ProductApiError> productApiErrors);

        void LogException(Exception exception);
    }
}