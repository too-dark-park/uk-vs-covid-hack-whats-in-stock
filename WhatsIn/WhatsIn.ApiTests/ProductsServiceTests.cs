using NUnit.Framework;
using Moq;
using WhatsIn.Services;
using WhatsIn.Services.Readers;
using WhatsIn.Services.Writers;

namespace WhatsIn.ApiTests
{
    public class ProductsServiceTests
    {
        private ProductsService _productsService;

        [SetUp]
        public void Setup()
        {
            var mockProductsReader = new Mock<IProductsReader>();
            var mockProductsWriter = new Mock<IProductsWriter>();
        }

        [Test]
        public void CanGetProductFromDb_ShouldReturnBeer()
        {
            Assert.Pass();
        }

        [Test]
        public void CanAddProduct_ShouldReturnBananas()
        {
            Assert.Pass();
        }


    }
}