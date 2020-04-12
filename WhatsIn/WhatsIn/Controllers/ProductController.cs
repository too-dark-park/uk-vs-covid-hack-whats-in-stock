using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WhatsIn.Helpers;
using WhatsIn.Services;

namespace WhatsIn.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private IProducts _products;
        private IPlaces _places;
        private IPosts _posts;

        public ProductController(IProducts products, IPlaces places, IPosts posts)
        {
            _products = products;
            _places = places;
            _posts = posts;
        }

        public IActionResult Add(string productName, string placeName, double? latitude, double? longitude)
        {
            if (string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(placeName))
            {
                return BadRequest();
            }

            if (!LocationHelper.IsValidLocation(latitude, longitude))
            {
                return BadRequest();
            }

            var place = _places.GetPlace(placeName);
            if (place == null)
            {
                place = _places.AddPlace(placeName, latitude.Value, longitude.Value);
            }

            var product = _products.GetProduct(productName);
            if (product == null)
            {
                product = _products.AddProduct(productName);
            };

            var post = _posts.Add(product.Id, place.Id);

            place.Posts.ToList().Add(post);
            product.Posts.ToList().Add(post);

            _places.UpdatePlace(place);
            _products.UpdateProduct(product);

            return Ok();
        }
    }
}
