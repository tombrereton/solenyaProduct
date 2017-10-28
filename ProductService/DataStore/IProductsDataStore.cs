namespace ProductService.DataStore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductService.Models;

    public interface IProductsDataStore
    {
        IEnumerable<PlpItem> GetAllPlpItems();

        Task RemoveDocumentCollection(string collectionName);

        Task CreateDocumentCollection(string collectionName);

        Task CreatePdpDocumentIfNotExists(string collectionName, PdpItem pdpItem);
    }
}