using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductService.Tests.Controllers;

namespace ProductService.Controllers
{
    public class PLPController : Controller
    {
        private readonly IProductsDataStore productDataStore;

        public PLPController(IProductsDataStore productDataStore)
        {
            this.productDataStore = productDataStore;
        }
    }
}
