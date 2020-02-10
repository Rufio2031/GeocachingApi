using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Infrastructure.Models
{
    public class GeocacheLocation : IGeocacheLocation
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
