namespace GeocodingService.Infraestructure.ThirdParties.Google.Models
{
    public class Result
    {
        public Address_Components[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

}
