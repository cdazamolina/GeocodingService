using GeocodingService.Core.DTO;
using GeocodingService.Core.Interfaces;
using GeocodingService.Infraestructure.ThirdParties.Google;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeocodingTests
{
    [TestClass]
    public class GeocodeTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IGeocode _geocode;
        public GeocodeTest()
        {
            _serviceProvider = Extensions
                .CreateHostBuilder(Array.Empty<string>())
                .Build()
                .Services;

            _geocode = _serviceProvider.GetService(typeof(IGeocode)) as IGeocode;
        }


        private readonly List<Geoposition> subaPolygon = new() 
        {
            new Geoposition()
            {
                Latitude = 4.696347,
                Longitude = -74.055354
            },
            new Geoposition()
            {
                Latitude = 4.725259,
                Longitude = - 74.125079
            },
            new Geoposition()
            {
                Latitude = 4.779147,
                Longitude = -74.093635
            },
            new Geoposition()
            {
                Latitude = 4.750578,
                Longitude = - 74.044368
            }
        };
        private readonly List<Geoposition> kennedyPolygon = new() 
        {
            new Geoposition()
            {
                Latitude = 4.629209312065549,
                Longitude = -74.12360213050236
            },
            new Geoposition()
            {
                Latitude = 4.595501536534566,
                Longitude = -74.1376783634125
            },
            new Geoposition()
            {
                Latitude = 4.596014864895202,
                Longitude = -74.16239760169377
            },
            new Geoposition()
            {
                Latitude = 4.632320161342013,
                Longitude = -74.17060053725866
            },
            new Geoposition()
            {
                Latitude = 4.65935354496937,
                Longitude = -74.13729823012976
            },
            new Geoposition()
            {
                Latitude = 4.644297104741083,
                Longitude = -74.12390864272741
            }
        };
        private readonly Geoposition subaLocation = new Geoposition { Latitude = 4.739336013793945, Longitude = -74.06344604492188 };
        private readonly Geoposition kennedyLocation = new Geoposition { Latitude = 4.627909, Longitude = -74.137408 };
        private readonly Geoposition usmeLocation = new Geoposition { Latitude = 4.509877, Longitude = -74.113487 };
        [TestMethod]
        public void IsPositionInsidePolygon()
        {
            bool resultSuba = _geocode.IsPointInsidePolygon(subaPolygon, subaLocation);
            bool resultKennedy = _geocode.IsPointInsidePolygon(kennedyPolygon, subaLocation);
            Assert.IsTrue(resultSuba);
            Assert.IsFalse(resultKennedy);

            bool resultSuba2 = _geocode.IsPointInsidePolygon(subaPolygon, kennedyLocation);
            bool resultKennedy2 = _geocode.IsPointInsidePolygon(kennedyPolygon, kennedyLocation);
            Assert.IsFalse(resultSuba2);
            Assert.IsTrue(resultKennedy2);

            bool resultSuba3 = _geocode.IsPointInsidePolygon(subaPolygon, usmeLocation);
            bool resultKennedy3 = _geocode.IsPointInsidePolygon(kennedyPolygon, usmeLocation);
            Assert.IsFalse(resultSuba3);
            Assert.IsFalse(resultKennedy3);
        }
    }
}