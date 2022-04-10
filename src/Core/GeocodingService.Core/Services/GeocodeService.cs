using GeocodingService.Core.DTO;
using GeocodingService.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocodingService.Core.Services
{
    public class GeocodeService : IGeocodeService
    {
        private readonly IGeocode _geocode;
        private readonly ILogger<GeocodeService> _logger;

        public GeocodeService(IGeocode geocode, ILogger<GeocodeService> logger)
        {
            _geocode = geocode ?? throw new ArgumentNullException(nameof(geocode));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<GeocodeInformation> GeocodeAddressWithOutFormat(string address) =>
            _geocode.GeocodeAddressWithOutFormat(address);

        public async Task<RouteCoverage> GetRouteCoverageToAddress(List<RouteCoverage> routeCoverages, string address)
        {
            var geocode = await _geocode.GeocodeAddressWithOutFormat(address);
            if (geocode.Status != "OK")
            {
                _logger.LogError($"Error locating address [{geocode.Status}].");
                return null;
            }

            foreach (RouteCoverage routeCoverage in routeCoverages)
                if (_geocode.IsPointInsidePolygon(routeCoverage.Positions, geocode.Geoposition))
                    return routeCoverage;
            return null;
        }
    }
}
