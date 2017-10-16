using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ProductService.DataStore;
using ProductService.Models;
using ProductService.Tests.Controllers;
using Swashbuckle.Swagger.Annotations;


namespace ProductService.Controllers
{
    public class PlpController : ApiController
    {
        private readonly IProductsDataStore _productDataStore;

        public PlpController(IProductsDataStore productDataStore)
        {
            this._productDataStore = productDataStore;
        }

        [System.Web.Mvc.Route("/")]
        [System.Web.Mvc.HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Products", typeof(List<PlpItem>))]
        public async Task<IHttpActionResult> GetItems()
        {
            var items = await this._productDataStore.GetAllItemsAsync();
            return this.Ok(items);

        }

    }
}
