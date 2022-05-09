using GeocodingService.Core.Interfaces;
using GeocodingService.Infraestructure.ThirdParties.Google.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;

namespace GeocodingService.Infraestructure.ThirdParties.Google
{
    public class Geocode : IGeocode
    {
        private readonly string _baseUrl;
        private readonly string _pathUrl;
        private readonly string _apiKey;

        public Geocode(IConfiguration configuration)
        {
            _baseUrl = configuration["ThirdParties:Google:GeocodeBaseUrl"];
            _pathUrl = configuration["ThirdParties:Google:GeocodePathUrl"];
            _apiKey = configuration["ThirdParties:Google:ApiKey"];
        }

        public async Task<Core.DTO.GeocodeInformation> GeocodeAddressWithOutFormat(string address)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest(_pathUrl, Method.Get);
            request.AddParameter(nameof(address), address);
            request.AddParameter("key", _apiKey);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) return new() { Status = "REQUEST_FAILED" };

            var geocodeInformation = JsonSerializer.Deserialize<GeocodeInformation>(response.Content);
            return geocodeInformation.ToDTO();
        }

        public bool IsPointInsidePolygon(List<Core.DTO.Geoposition> polygon, Core.DTO.Geoposition point)
        {
            double minX = polygon[0].Latitude;
            double maxX = polygon[0].Latitude;
            double minY = polygon[0].Longitude;
            double maxY = polygon[0].Longitude;
            for (int i = 1; i < polygon.Count; i++)
            {
                Core.DTO.Geoposition q = polygon[i];
                minX = Math.Min(q.Latitude, minX);
                maxX = Math.Max(q.Latitude, maxX);
                minY = Math.Min(q.Longitude, minY);
                maxY = Math.Max(q.Longitude, maxY);
            }
            if (point.Latitude < minX || point.Latitude > maxX || point.Longitude < minY || point.Longitude > maxY)
            {
                return false;
            }

            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Longitude > point.Longitude) != (polygon[j].Longitude > point.Longitude) &&
                     point.Latitude < (polygon[j].Latitude - polygon[i].Latitude) * (point.Longitude - polygon[i].Longitude) / (polygon[j].Longitude - polygon[i].Longitude) + polygon[i].Latitude)
                {
                    inside = !inside;
                }
            }
            return inside;
        }
    }
}
