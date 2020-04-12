using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WhatsIn.Models;
using WhatsIn.Services.Readers;
using WhatsIn.Services.Writers;

namespace WhatsIn.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsReader _productsReader;
        private readonly IProductsWriter _productsWriter;

        public ProductsService(IProductsReader productsReader, IProductsWriter productsWriter)
        {
            _productsReader = productsReader;
            _productsWriter = productsWriter;
        }

        public Product AddProduct(string productName)
        {
            var product = new Product()
            {
                Name = productName
            };

            var productToAdd = _productsWriter.AddProductToDb(product);

            return productToAdd;
        }

        public int? GetId(string productName)
        {
            var product = _productsReader.GetProductFromDbByName(productName);

            if (product != null)
                return product.Id;

            return null;
        }

        public Product GetProduct(string productName)
        {
            var product = _productsReader.GetProductFromDbByName(productName);

            if (product != null)
                return product;

            return null;
        }

        public Product GetProduct(int productId)
        {
            var product = _productsReader.GetProductFromDbById(productId);

            if (product != null)
                return product;

            return null;
        }

        public IEnumerable<int> GetWildCardIds(string productName)
        {
            var wildCardIds = _productsReader.GetWildCardIdsFromDb(productName);

            return wildCardIds;
        }

        public void UpdateProduct(Product product)
        {
            _productsWriter.UpdateExistingProductInDb(product);
        }
    }
}
