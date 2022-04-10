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
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Latitude < point.Latitude &&
                    polygon[j].Latitude >= point.Latitude ||
                    polygon[j].Latitude < point.Latitude &&
                    polygon[i].Latitude >= point.Latitude)
                {
                    if (polygon[i].Longitude + (point.Latitude - polygon[i].Latitude) / (polygon[j].Latitude - polygon[i].Latitude) * (polygon[j].Longitude - polygon[i].Longitude) < point.Longitude)
                    {
                        return false;
                    }
                }
                j = i;
            }
            return true;
        }
    }
}
