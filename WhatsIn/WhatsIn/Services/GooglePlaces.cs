using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WhatsIn.Services.Models;

namespace WhatsIn.Services
{
    public class GooglePlaces : IPlaces
    {
        private MapApiSettings _mapApiSettings;
        private IMemoryCache _cache;

        // TODO discuss the opennow=true and radius parameters
        private const string _apiUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?radius=100";

        public GooglePlaces(IOptions<MapApiSettings> mapApiSettings, IMemoryCache memoryCache)
        {
            _mapApiSettings = mapApiSettings.Value;
            _cache = memoryCache;
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
                            places.Add(new PlaceDto() { Name = token["name"].ToString() }) ;
                        }
                    }
                }
            }
            catch(Exception e)
            {
            }

            return places;
        }
    }
}
