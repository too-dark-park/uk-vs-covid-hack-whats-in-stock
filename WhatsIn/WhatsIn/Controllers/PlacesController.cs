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
        /// <returns>
        /// JSON collection of places within a certain radius (nearby)
        /// Google maps limits this to 20
        ///
        /// <example>
        /// <code>
        ///
        /// [{"Name":"Marks & Spencer BRISTOL BROADMEAD","Latitude":51.45808539999999,"Longitude":-2.590739},{"Name":"Taste of Napoli","Latitude":51.45815009999999,"Longitude":-2.5907088}]
        /// </code>
        /// </example>
        /// </returns>
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
