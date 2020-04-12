using System.Collections.Generic;
using WhatsIn.Services.Models;

namespace WhatsIn.Services
{
    public interface IPlaces
    {
        IEnumerable<PlaceDto> GetNearbyPlaces(double latitude, double longitude);

        IEnumerable<PlaceDto> MapToDto(string responseFromServer);
    }
}
