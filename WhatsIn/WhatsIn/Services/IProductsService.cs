using System.Collections.Generic;
using WhatsIn.Models;

namespace WhatsIn.Services
{
    public interface IProductsService
    {
        Product AddProduct(string productName);

        int? GetId(string productName);

        Product GetProduct(string productName);

        void UpdateProduct(Product product);

        IEnumerable<int> GetWildCardIds(string productName);

        Product GetProduct(int productId);
    }
}
