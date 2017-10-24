namespace ProductService.DataStore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Azure.Documents.Client;

    using Newtonsoft.Json;

    using ProductService.Models;

    /// <summary>
    /// The product data store.
    /// </summary>
    public class ProductDataStore : IProductsDataStore
    {
        private DocumentClient _client;

        public ProductDataStore(DocumentClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// The get all items async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<PlpItem>> GetAllItemsAsync()
        {
            return null;
        }
    }
}