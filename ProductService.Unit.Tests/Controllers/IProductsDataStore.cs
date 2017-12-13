using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Tests.Controllers
{
    internal interface IProductsDataStore
    {
        Task<IEnumerable<PLPItem>> GetAllItemsAsync();
    }
}