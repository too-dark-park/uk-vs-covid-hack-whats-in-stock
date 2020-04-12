using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WhatsIn.Models;

namespace WhatsIn.Services.Readers
{
    public class ProductsReader : IProductsReader
    {

        private readonly WhatsInContext _context;

        public ProductsReader(WhatsInContext context)
        {
            _context = context;
        }

        public int? GetProductIdFromDb(string productName)
        {
            var product = _context.Products.SingleOrDefault(x => x.Name == productName);

            if (product != null)
                return product.Id;

            return null;
        }

        public Product GetProductFromDbByName(string productName)
        {
            var product = _context.Products.SingleOrDefault(x => x.Name == productName);

            return product;
        }

        public Product GetProductFromDbById(int productId)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == productId);

            if (product != null)
                return product;

            return null;
        }

        public IEnumerable<int> GetWildCardIdsFromDb(string productName)
        {
            var wildcard = Regex.Replace(productName, @"\s+", "%");

            var t = from x in _context.Products
                    where EF.Functions.Like(x.Name, $"%{wildcard}%")
                    select x.Id;

            return t.AsEnumerable();
        }
    }
}
