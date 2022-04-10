namespace GeocodingService.Core.Interfaces
{
    public interface IGeocode
    {
        /// <summary>
        /// Retorna la latitud y longitud de posición apartir de una dirección dada.
        /// </summary>
        /// <param name="address">Dirección sin formato.</param>
        /// <returns>
        /// found => Indica si la dirección fue encontrada por Google.
        /// geoposition => Latitud y longuitud de la dirección.
        /// </returns>
        public Task<DTO.GeocodeInformation> GeocodeAddressWithOutFormat(string address);

        /// <summary>
        /// Retorna una confirmación, de si una posición (Latitud y longuitud) está localizado
        /// en el interior de un poligono de psiciones o ruta.
        /// </summary>
        /// <param name="polygon">Poligono de la ruta.</param>
        /// <param name="point">Punto de la ubicación a validar.</param>
        /// <returns></returns>
        public bool IsPointInsidePolygon(List<DTO.Geoposition> polygon, DTO.Geoposition point);
    }
}
