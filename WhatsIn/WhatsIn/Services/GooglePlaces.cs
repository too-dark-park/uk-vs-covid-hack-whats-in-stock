using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WhatsIn.Models;
using WhatsIn.Services.Models;

namespace WhatsIn.Services
{
    public class GooglePlaces : IPlaces
    {
        // TODO google should be split out into its own API service

        private MapApiSettings _mapApiSettings;
        private IMemoryCache _cache;
        private readonly WhatsInContext _context;

        // TODO discuss the opennow=true and radius parameters
        private const string _apiUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?radius=100";

        public GooglePlaces(IOptions<MapApiSettings> mapApiSettings, IMemoryCache memoryCache, WhatsInContext context)
        {
            _mapApiSettings = mapApiSettings.Value;
            _cache = memoryCache;
            _context = context;
        }

        public IEnumerable<PlaceDto> GetNearbyPlaces(double latitude, double longitude)
        {
            IEnumerable<PlaceDto> cacheEntry;
            string key = $"{latitude},{longitude}";

            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = CallApi(latitude, longitude);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        private IEnumerable<PlaceDto> CallApi(double latitude, double longitude)
        {
            var url = $"{_apiUrl}&key={_mapApiSettings.MapApiKey}&location={latitude},{longitude}";

            var request = WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return MapToDto(responseFromServer);
        }

        public IEnumerable<PlaceDto> MapToDto(string responseFromServer)
        {
            var places = new List<PlaceDto>();

            try
            {
                var placesResponse = JsonConvert.DeserializeObject<JObject>(responseFromServer);

                if (placesResponse["results"] != null)
                {
                    foreach(JToken token in placesResponse["results"])
                    {
                        if (token["name"] != null)
                        {
                            places.Add(new PlaceDto() {
                                Name = token["name"].ToString(),
                                Latitude = double.Parse(token["geometry"]["location"]["lat"].ToString()),
                                Longitude = double.Parse(token["geometry"]["location"]["lng"].ToString()),
                            });
                        }
                    }
                }
            }
            catch(Exception e)
            {
            }

            return places;
        }

        public int? GetId(string placeName)
        {
            var place = _context.Places.SingleOrDefault(x => x.Name == placeName);

            if (place != null)
                return place.Id;

            return null;
        }

        public Place AddPlace(string placeName, double latitude, double longitude)
        {
            var place = new Place()
            {
                Name = placeName,
                Latitude = latitude,
                Longitude = longitude
            };

            _context.Places.Add(place);
            _context.SaveChanges();

            return place;
        }

        public Place GetPlace(string placeName)
        {
            var place = _context.Places.SingleOrDefault(x => x.Name == placeName);

            if (place != null)
                return place;

            return null;
        }

        public void UpdatePlace(Place place)
        {
            _context.Places.Update(place);
            _context.SaveChanges();
        }

        public Place GetPlace(int productId)
        {
            var place = _context.Places.SingleOrDefault(x => x.Id == productId);

            if (place != null)
                return place;

            return null;
        }
    }
}
