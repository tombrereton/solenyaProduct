using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Castle.Core.Internal;
using ProductService.DataStore;
using ProductService.Models;
using ProductService.Tests.Controllers;
using Swashbuckle.Swagger.Annotations;


namespace ProductService.Controllers
{
    using System.Configuration;

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

            var x = ConfigurationManager.AppSettings["DatabaseUrl"];

            var items = await this._productDataStore.GetAllItemsAsync() as List<PlpItem>;
            if (items.IsNullOrEmpty())
            {
                return this.NotFound();
            }
            return this.Ok(items);
        }

    }
}
