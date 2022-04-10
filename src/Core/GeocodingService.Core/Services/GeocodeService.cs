using GeocodingService.Core.DTO;
using GeocodingService.Core.Interfaces;
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

        public GeocodeService(IGeocode geocode)
        {
            _geocode = geocode ?? throw new ArgumentNullException(nameof(geocode));
        }

        public Task<GeocodeInformation> GeocodeAddressWithOutFormat(string address) =>
            _geocode.GeocodeAddressWithOutFormat(address);

        public async Task<RouteCoverage> GetRouteCoverageToAddress(List<RouteCoverage> routeCoverages, string address)
        {
            var geocode = await _geocode.GeocodeAddressWithOutFormat(address);
            if (geocode.Status != "OK")
            {
                return null;
            }

            foreach (RouteCoverage routeCoverage in routeCoverages)
                if (_geocode.IsPointInsidePolygon(routeCoverage.Positions, geocode.Geoposition))
                    return routeCoverage;
            return null;
        }
    }
}
