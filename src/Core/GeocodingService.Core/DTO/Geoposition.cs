using System.ComponentModel.DataAnnotations;

namespace GeocodingService.Core.DTO
{
    public class Geoposition
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
