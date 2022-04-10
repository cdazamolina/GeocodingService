using AutoMapper;
using GeocodingService.Core.Interfaces;
using GeocodingService.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GeocodingService.Core
{
    public static class Extensions
    {
        private static readonly string _seqSectionPath = "SeqService";

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IGeocodeService, GeocodeService>();

            IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq(configuration.GetSection(_seqSectionPath));
            });
            return services;
        }
    }
}
