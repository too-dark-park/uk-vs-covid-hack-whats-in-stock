using WhatsIn.Models;

namespace WhatsIn.Services
{
    public interface IPosts
    {
        Post Add(int productId, int placeId);
    }
}
