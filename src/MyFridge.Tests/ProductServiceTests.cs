using FluentAssertions;
using MyFridge.Services;
using NUnit.Framework;

namespace MyFridge.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new ProductService();
        }

        [Test]
        public async void should_load_coca_cola_by_barcode()
        {

            var productName = await _service.FindByBarcode("5449000050205");

            productName.Should().NotBeNull();
            productName.Should().Be("Coca Cola Light 33-cL Can");
        }
    }
}