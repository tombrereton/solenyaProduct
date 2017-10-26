namespace ProductService.Tests.DataStore
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
            this._localProductDataStore = new LocalProductDataStore();
        }

        [Test]
        public void ReturnExactListOfItems()
        {
            var itemsFromDataStore = this._localProductDataStore.GetAllPlpItems();

            var result = TestData.GetLocalItems();

            CollectionAssert.AreEqual(itemsFromDataStore.ToList(), result);
        }
    }
}