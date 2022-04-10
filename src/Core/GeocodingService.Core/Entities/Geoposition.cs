using System.ComponentModel.DataAnnotations;

namespace GeocodingService.Core.Entities
{
    public class Geoposition
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public long RouteCoverageId { get; set; }
        public RouteCoverage RouteCoverage { get; set; }
    }
}
