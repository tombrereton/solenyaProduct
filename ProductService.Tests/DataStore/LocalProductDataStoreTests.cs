namespace ProductService.Tests.DataStore
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Models;

    [TestFixture]
    public class LocalProductDataStoreTests
    {
        private LocalProductDataStore _localProductDataStore;

        [SetUp]
        public void SetUp()
        {
            this._localProductDataStore = new LocalProductDataStore();
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = this._localProductDataStore.GetAllPlpItemsAsync();

            var result = TestData.GetItems();

            CollectionAssert.AreEqual(itemsFromDataStore.Result.ToList(), result);
        }
    }
}