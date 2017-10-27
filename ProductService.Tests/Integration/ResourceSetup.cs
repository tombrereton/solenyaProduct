using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

using ProductService.Models;
using ProductService.Tests.DataStore;

namespace ProductService.Tests.Integration
{
    public class ResourceSetUp
    {
        private DocumentClient _client;

        private List<PlpItem> _items;

        private readonly string documentDbName;

        private readonly string documentDbCollection;

        private PlpItem testItem = new PlpItem()
                                       {
                                           ProductId = 1,
                                           ProductName = "Warehouse Side Split Roll Neck Jumper",
                                           SplashImgUrl = "./static/media/prod1-img1.jpg",
                                           Price = 4600,
                                           DiscountPrice = null
                                       };

        public ResourceSetUp()
        {
            this._items = TestData.GetItems() as List<PlpItem>;
            this._client = new DocumentClient(
                new Uri("https://team-solenya-rg-dev-cosmos.documents.azure.com:443/"),
                "lZe5xiY9eaeFq54racXVZm3tVBA5FW3GIXvc7W4zkTVS3JQLy0yHYSYt7vekAN1p7kYIxj4avg3QsukEJLHv2Q==");
            this.documentDbName = ConfigurationManager.AppSettings["DocumentDBName"];
            this.documentDbCollection = "";
        }

        public async Task SetUpDb()
        {
            await this._client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(this.documentDbName),
                new DocumentCollection { Id = "product_collection_test" }).ConfigureAwait(false);

            foreach (PlpItem item in this._items)
            {
                try
                {
                    await this._client.ReadDocumentAsync(
                        UriFactory.CreateDocumentUri(
                            this.documentDbName,
                            this.documentDbCollection,
                            item.ProductId.ToString())).ConfigureAwait(false);
                }
                catch (DocumentClientException de)
                {
                    if (de.StatusCode == HttpStatusCode.NotFound)
                    {
                        await this._client.CreateDocumentAsync(
                            UriFactory.CreateDocumentCollectionUri(this.documentDbName, this.documentDbCollection),
                            item).ConfigureAwait(false);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public async Task TearDown()
        {
            try
            {
                await this._client.DeleteDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(this.documentDbName, this.documentDbCollection)).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
    }
}