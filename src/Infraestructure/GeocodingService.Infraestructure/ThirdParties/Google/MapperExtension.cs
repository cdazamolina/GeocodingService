using GeocodingService.Core.DTO;

namespace GeocodingService.Infraestructure.ThirdParties.Google
{
    internal static class MapperExtension
    {
        internal static Core.DTO.GeocodeInformation ToDTO(this Models.GeocodeInformation geocodeInformation)
        {
            Geoposition geoposition = (geocodeInformation.status == "OK") ?
                new()
                { 
                    Latitude = geocodeInformation.results[0].geometry.location.lat,
                    Longitude = geocodeInformation.results[0].geometry.location.lng
                } : null;

            return new Core.DTO.GeocodeInformation()
            {
                Status = geocodeInformation.status,
                Geoposition = geoposition
            };
        }
    }
}
