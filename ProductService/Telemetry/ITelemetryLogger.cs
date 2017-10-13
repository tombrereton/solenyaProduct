using System;
using System.Collections.Generic;

namespace ProductService.Tests.Controllers
{
    public interface ITelemetryLogger
    {
        void LogValidationErrors(IList<ValidationError> validationErrors);

        void LogException(Exception exception);
    }
}