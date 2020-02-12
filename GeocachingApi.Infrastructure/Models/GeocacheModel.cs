using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Infrastructure.Models
{
    public class GeocacheModel : IGeocacheModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
    }
}
