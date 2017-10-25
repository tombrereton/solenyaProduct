namespace ProductService.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;

    using Castle.Windsor.Installer;

    using Microsoft.Azure.Documents.Client;

    using Newtonsoft.Json;

    using ProductService.Models;

    /// <summary>
    /// The product data store.
    /// </summary>
    public class ProductDataStore : IProductsDataStore
    {
        private string EndpointUrl = ConfigurationManager.AppSettings["DocumentDBEndpoint"];

        private string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"];

        private DocumentClient _client;

        public ProductDataStore()
        {
        }

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
            this._client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<PlpItem> productsQueryInSql = this._client.CreateDocumentQuery<PlpItem>(
                UriFactory.CreateDocumentCollectionUri("team-solenya-dev-db", "products"),
                "SELECT * FROM products",
                queryOptions);
            Console.WriteLine("Query data:", productsQueryInSql);
            
            return productsQueryInSql;
        }
    }
}