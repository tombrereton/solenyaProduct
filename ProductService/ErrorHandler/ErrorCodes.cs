using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductService.ErrorHandler
{
    public class ErrorCodes
    {
        public static string ProductNotFoundCode = "ProductItemDoesNotExist";

        public static string ProductOrCollectionNotFoundCode = "ProductItemOrCollectionNameDoesNotExist";

        public static string ProductNotFoundInDatabaseMsg = "Product item was not found in the database.";

        public static string CollectionNotFoundMsg = "Product item or collection name was not found in the database.";
    }
}