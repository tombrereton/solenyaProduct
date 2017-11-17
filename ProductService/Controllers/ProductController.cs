namespace ProductService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Castle.Core.Internal;

    using Microsoft.Azure.Documents.SystemFunctions;

    using ProductService.DataStore;
    using ProductService.ErrorHandler;
    using ProductService.Models;
    using ProductService.Tests.Controllers;

    public class ProductController : ApiController
    {
        private readonly IProductsDataStore _productDataStore;
        private readonly ProductApiErrorHandler _errorHandler;

        // private readonly ItemValidator _validator = new ItemValidator();
        public ProductController(IProductsDataStore productDataStore, ITelemetryLogger telemetryLogger)
        {
            this._productDataStore = productDataStore;
            this._errorHandler = new ProductApiErrorHandler(this, telemetryLogger);
        }


        [Route("")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetItems(string collectionName = "products")
        {
            var items = this._productDataStore.GetAllPlpItemsFromCollection(collectionName) as List<PlpItem>;
            if (items.IsNullOrEmpty())
            {
                return this.NotFound();
            }

            return this.Ok(items);
        }

        [Route("products/{id}")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetItem([FromUri] int id, string collectionName = "products")
        {
            var item = this._productDataStore.GetPdpItemFromCollection(id, collectionName);

            var errors = ProductApiErrorHandler.Execute(item);

            if (errors.Any())
            {

                this._errorHandler.HandleFailedProductApiResponse(errors);
                return this.Ok(errors);
            }

            return this.Ok(item);
        }

        [Route("test")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Test()
        {
            var testString = "Hello, this is a test";
            return this.Ok(testString);
        }
    }
}