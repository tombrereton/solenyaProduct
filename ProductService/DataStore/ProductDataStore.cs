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
        private readonly DocumentClient _client;

        private readonly string _endPointUrl;

        private readonly string _primaryKey;

        private readonly string _documentDbName;

        public ProductDataStore()
        {
        }

        public ProductDataStore(string endPointUrl, string primaryKey)
        {
            this._endPointUrl = endPointUrl;
            this._primaryKey = primaryKey;
            this._client = new DocumentClient(new Uri(this._endPointUrl), this._primaryKey);
            this._documentDbName = ConfigurationManager.AppSettings["DocumentDBName"];

        }

        /// <summary>
        /// The get all items async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<PlpItem>> GetAllPlpItemsAsync()
        {
            var documentDBCollection = "products";
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<PlpItem> productsQueryInSql = this._client.CreateDocumentQuery<PlpItem>(
                UriFactory.CreateDocumentCollectionUri(this._documentDbName, documentDBCollection),
                "SELECT * FROM products",
                queryOptions);

            return productsQueryInSql;
        }
    }
}