using GeocodingService.Core.Interfaces;
using GeocodingService.Infraestructure.ThirdParties.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocodingTests
{
    public static class Extensions
    {
        private static readonly string _settingFile = "appSettings.json";

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).AddConfigurations().AddDependencyInjections();

        private static IHostBuilder AddConfigurations(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureHostConfiguration(config =>
            {
                config.AddJsonFile(Path.Combine(GetCurrentDirectory(), _settingFile), optional: false);
            });

        private static IHostBuilder AddDependencyInjections(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureServices((hostServices, services) =>
            {
                services.AddScoped<IGeocode, Geocode>();
            });

        private static string GetCurrentDirectory() => Directory.GetCurrentDirectory();

    }
}
