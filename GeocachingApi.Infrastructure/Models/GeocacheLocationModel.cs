using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Infrastructure.Models
{
    public class GeocacheLocationModel : IGeocacheLocationModel
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
