using WhatsIn.Models;

namespace WhatsIn.Services.Writers
{
    public class ProductsWriter : IProductsWriter
    {
        private WhatsInContext _context;

        public ProductsWriter(WhatsInContext context)
        {
            _context = context;
        }

        public Product AddProductToDb(string productName)
        {
            var product = new Product()
            {
                Name = productName
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public void UpdateExistingProductInDb(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }

}
