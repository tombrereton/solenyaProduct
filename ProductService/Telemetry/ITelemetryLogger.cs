using System;
using System.Collections.Generic;
using ProductService.Models;

namespace ProductService.Tests.Controllers
{
    public interface ITelemetryLogger
    {
        void LogApiErrors(IList<ProductApiError> productApiErrors);

        void LogException(Exception exception);
    }
}