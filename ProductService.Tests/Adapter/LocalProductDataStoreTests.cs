namespace ProductService.Tests.Adapter
{
    using System.Linq;

    using NUnit.Framework;

    using ProductService.DataStore;
    using ProductService.Tests.TestData;

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
            var itemsFromDataStore = this._localProductDataStore.GetAllPlpItemsFromCollection("test_data_product");

            var result = TestData.GetLocalItems();

            CollectionAssert.AreEqual(itemsFromDataStore.ToList(), result);
        }
    }
}