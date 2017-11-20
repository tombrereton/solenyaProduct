namespace ProductService.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Microsoft.ApplicationInsights;

    using ProductService.DataStore;
    using ProductService.ErrorHandler;
    using ProductService.Logger;
    using ProductService.Models;

    public class ProductController : ApiController
    {
        private readonly IProductsDataStore _productDataStore;

        private readonly ITelemetryLogger _logger;

        public ProductController(IProductsDataStore productDataStore)
        {
            this._productDataStore = productDataStore;
            this._logger = new TelemetryLogger(new TelemetryClient());
        }

        [Route("")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetItems(string collectionName = "products")
        {
            var items = this._productDataStore.GetAllPlpItemsFromCollection(collectionName) as List<PlpItem>;

            var errors = ProductApiErrorHandler.Execute(items);

            if (errors.Any())
            {
                this._logger.LogApiErrors(errors);
                return this.Ok(errors);
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
                this._logger.LogApiErrors(errors);
                return this.Ok(errors);
            }

            return this.Ok(item);
        }
    }
}