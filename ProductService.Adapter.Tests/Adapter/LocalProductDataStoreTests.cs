namespace ProductService.Adapter.Tests
{
    using System.Linq;
    using NUnit.Framework;
    using ProductService.DataStore;

    [TestFixture]
    public class LocalProductDataStoreTests
    {
        private LocalProductDataStore _localProductDataStore;

        [SetUp]
        public void SetUp()
        {
            _localProductDataStore = new LocalProductDataStore();
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = _localProductDataStore.GetAllPlpItemsFromCollection("test_data_product");

            var result = TestData.GetLocalItems();

            CollectionAssert.AreEqual(itemsFromDataStore.ToList(), result);
        }
    }
}