namespace ProductService.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Castle.Core.Internal;

    using Microsoft.Azure.Documents.SystemFunctions;

    using ProductService.DataStore;
    using ProductService.Models;

    public class ProductController : ApiController
    {
        private readonly IProductsDataStore _productDataStore;

        public ProductController(IProductsDataStore productDataStore)
        {
            this._productDataStore = productDataStore;
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

        [Route("product={id}")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetItem(int id, string collectionName = "products")
        {
            var item = this._productDataStore.GetPdpItemFromCollection(collectionName, id) as PdpItem;

            if (item == null)
            {
                return this.NotFound();
            }

            return this.Ok(item);

        }
    }
}