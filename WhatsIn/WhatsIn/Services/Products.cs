using System.Linq;
using WhatsIn.Models;

namespace WhatsIn.Services
{
    public class Products : IProducts
    {
        private readonly WhatsInContext _context;

        public Products(WhatsInContext context)
        {
            _context = context;
        }

        public Product AddProduct(string productName)
        {
            var product = new Product()
            {
                Name = productName
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public int? GetId(string productName)
        {
            var product = _context.Products.SingleOrDefault(x => x.Name == productName);

            if (product != null)
                return product.Id;

            return null;
        }

        public Product GetProduct(string productName)
        {
            var product = _context.Products.SingleOrDefault(x => x.Name == productName);

            if (product != null)
                return product;

            return null;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
