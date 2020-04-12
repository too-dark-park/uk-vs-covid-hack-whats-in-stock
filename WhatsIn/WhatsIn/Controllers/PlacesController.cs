using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatsIn.Services;

namespace WhatsIn.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PlacesController : ControllerBase
    {
        private IPlaces _places;

        public PlacesController(IPlaces places)
        {
            _places = places;
        }

        public IActionResult Nearby(double? latitude, double? longitude)
        {
            if (latitude == null || longitude == null)
            {
                return BadRequest();
            }

            // Latitude. May range from -90.0 to 90.0
            if (latitude < -90 || latitude > 90)
            {
                return BadRequest();
            }

            // Longitude. May range from -180.0 to 180.0
            if (longitude < -180 || latitude > 180)
            {
                return BadRequest();
            }


            return Ok(JsonConvert.SerializeObject(_places.GetNearbyPlaces(latitude.Value, longitude.Value)));
        }
    }
}
