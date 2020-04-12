using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WhatsIn.Helpers;
using WhatsIn.Models;
using WhatsIn.Services;
using WhatsIn.Services.Models;

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
            try
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
            catch(Exception e)
            {
                // log e
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult FindProducts(string productName)
        {
            IEnumerable<int> productIds = _products.GetWildCardIds(productName);
            IEnumerable<Post> posts = _posts.GetProductPosts(productIds);

            var results = new List<SearchResult>();

            // FIXME this is inefficient
            foreach (var post in posts.ToList())
            {
                var place = _places.GetPlace(post.PlaceId);
                var product = _products.GetProduct(post.ProductId);

                results.Add(new SearchResult()
                {
                    PlaceName = place.Name,
                    ProductName = product.Name,
                    Longitude = place.Longitude,
                    Latitude = place.Latitude
                });
            }

            return Ok(JsonConvert.SerializeObject(results));
        }
    }
}
