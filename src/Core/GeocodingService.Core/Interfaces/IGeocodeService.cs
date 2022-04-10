using GeocodingService.Core.DTO;

namespace GeocodingService.Core.Interfaces
{
    public interface IGeocodeService
    {
        public Task<RouteCoverage> GetRouteCoverageToAddress(List<RouteCoverage> routeCoverages, string address);
        public Task<GeocodeInformation> GeocodeAddressWithOutFormat(string address);
    }
}
