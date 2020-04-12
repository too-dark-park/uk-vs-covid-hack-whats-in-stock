using WhatsIn.Models;

namespace WhatsIn.Services
{
    public interface IProducts
    {
        Product AddProduct(string productName);

        int? GetId(string productName);

        Product GetProduct(string productName);

        void UpdateProduct(Product product);
    }
}
