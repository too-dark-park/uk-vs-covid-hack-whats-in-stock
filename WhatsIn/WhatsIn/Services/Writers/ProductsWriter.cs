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

        public Product AddProductToDb(Product productToAdd)
        {
            _context.Products.Add(productToAdd);
            _context.SaveChanges();

            return productToAdd;
        }

        public void UpdateExistingProductInDb(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }

}
