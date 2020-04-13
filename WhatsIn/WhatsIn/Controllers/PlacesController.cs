using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatsIn.Helpers;
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

        /// <summary>
        /// <c>Nearby</c> returns a collection of places near the provided coordinates
        /// Route: <c>/Places/AddNearby</c>
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public IActionResult Nearby(double? latitude, double? longitude)
        {
            if (!LocationHelper.IsValidLocation(latitude, longitude))
            {
                return BadRequest();
            }

            return Ok(JsonConvert.SerializeObject(_places.GetNearbyPlaces(latitude.Value, longitude.Value)));
        }
    }
}
