namespace ProductService.DataStore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProductService.Models;

    public interface IProductsDataStore
    {
        IEnumerable<PlpItem> GetAllPlpItems();
    }
}