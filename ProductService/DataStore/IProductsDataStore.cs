using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Tests.Controllers
{
    public interface IProductsDataStore
    {
        Task<IEnumerable<PLPItem>> GetAllItemsAsync();
    }
}