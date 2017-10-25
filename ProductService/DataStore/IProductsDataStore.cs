using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.DataStore
{
    public interface IProductsDataStore
    {
        Task<IEnumerable<PlpItem>> GetAllPlpItemsAsync();
    }
}