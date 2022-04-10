using GeocodingService.Core.Interfaces;
using GeocodingService.Infraestructure.ThirdParties.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeocodingService.Infraestructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {
            services.AddScoped<IGeocode, Geocode>();
            return services;
        }
    }
}
