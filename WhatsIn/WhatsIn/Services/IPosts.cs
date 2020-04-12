using System.Collections.Generic;
using WhatsIn.Models;

namespace WhatsIn.Services
{
    public interface IPosts
    {
        Post Add(int productId, int placeId);
        IEnumerable<Post> GetProductPosts(IEnumerable<int> productIds);
    }
}
