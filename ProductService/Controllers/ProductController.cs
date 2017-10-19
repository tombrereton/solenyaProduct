using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using ProductService.DataStore;
using ProductService.Models;
using ProductService.Tests.Controllers;
using Swashbuckle.Swagger.Annotations;


namespace ProductService.Controllers
{
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
        public async Task<IHttpActionResult> GetItems()
        {
            var items = await this._productDataStore.GetAllItemsAsync() as List<PlpItem>;
            return this.Ok(items);
        }

    }
}
