namespace ProductService.ErrorHandler
{
    using System.Collections.Generic;

    using ProductService.Models;

    public class ProductApiErrorHandler
    {
        public static List<ProductApiError> Execute(PdpItem item)
        {
            var errors = new List<ProductApiError>();

            if (item == null)
            {
                var error = new ProductApiError("PdpItemDoesNotExist", "Pdp item was not found in the database.");
                errors.Add(error);
            }

            return errors;
        }
    }
}