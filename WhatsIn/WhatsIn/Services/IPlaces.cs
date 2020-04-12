using System.Collections.Generic;
using WhatsIn.Models;
using WhatsIn.Services.Models;

namespace WhatsIn.Services
{
    public interface IPlaces
    {
        IEnumerable<PlaceDto> GetNearbyPlaces(double latitude, double longitude);

        int? GetId(string placeName);

        Place GetPlace(string placeName);

        Place AddPlace(string placeName, double latitude, double longitude);

        IEnumerable<PlaceDto> MapToDto(string responseFromServer);

        void UpdatePlace(Place place);

        Place GetPlace(int productId);
    }
}
