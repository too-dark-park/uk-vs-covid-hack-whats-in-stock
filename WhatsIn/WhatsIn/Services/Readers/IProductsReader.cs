using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsIn.Models;

namespace WhatsIn.Services.Readers
{
    public interface IProductsReader
    {
        int? GetProductIdFromDb(string productName);

        Product GetProductFromDbByName(string productName);

        IEnumerable<int> GetWildCardIdsFromDb(string productName);

        Product GetProductFromDbById(int productId);
    }
}
