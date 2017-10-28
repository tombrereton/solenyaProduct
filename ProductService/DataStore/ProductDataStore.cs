namespace ProductService.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    using ProductService.Models;

    /// <summary>
    /// The product data store.
    /// </summary>
    public class ProductDataStore : IProductsDataStore
    {
        private readonly string _endPointUrl;

        private readonly string _primaryKey;

        private readonly string _documentDbName;

        /// <summary>
        /// Gets the _client.
        /// </summary>
        public DocumentClient _client { get; }

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
        public IEnumerable<PlpItem> GetAllPlpItems()
        {
            var documentDBCollection = "products";
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<PlpItem> productsQueryInSql = this._client.CreateDocumentQuery<PlpItem>(
                UriFactory.CreateDocumentCollectionUri(this._documentDbName, documentDBCollection),
                "SELECT * FROM products",
                queryOptions);

            return productsQueryInSql.ToList<PlpItem>();
        }

        public async Task RemoveDocumentCollection(string collectionName)
        {
            try
            {
                await this._client
                    .DeleteDocumentCollectionAsync(
                        UriFactory.CreateDocumentCollectionUri(this._documentDbName, collectionName))
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Deleting collection in DB exception: {0}", exception);
            }
        }

        public async Task CreateDocumentCollection(string collectionName)
        {
            await this._client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(this._documentDbName),
                new DocumentCollection { Id = "test_data" }).ConfigureAwait(false);
        }

        public async Task CreatePdpDocumentIfNotExists(string collectionName, PdpItem pdpItem)
        {
            try
            {
                await this._client.ReadDocumentAsync(
                        UriFactory.CreateDocumentUri(
                            this._documentDbName,
                            collectionName,
                            pdpItem.ProductId.ToString()))
                    .ConfigureAwait(false);
                Console.WriteLine("Found {0}", pdpItem.ProductId);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this._client.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri(this._documentDbName, collectionName),
                        pdpItem).ConfigureAwait(false);
                    Console.WriteLine("Created PdpItem {0}", pdpItem.ProductId);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}