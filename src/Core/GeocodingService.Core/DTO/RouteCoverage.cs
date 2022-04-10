using System.ComponentModel.DataAnnotations;

namespace GeocodingService.Core.DTO
{
    public class RouteCoverage
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public List<Geoposition> Positions { get; set; } 
    }
}
