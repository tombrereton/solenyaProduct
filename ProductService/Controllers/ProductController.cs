namespace ProductService.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Castle.Core.Internal;

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
        public IHttpActionResult GetItems()
        {
            var items = this._productDataStore.GetAllPlpItems() as List<PlpItem>;
            if (items.IsNullOrEmpty())
            {
                return this.NotFound();
            }

            return this.Ok(items);
        }
    }
}