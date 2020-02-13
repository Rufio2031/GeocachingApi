using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Infrastructure.Models
{
    public class GeocacheItemPatchGeocacheIdModel : IGeocacheItemPatchGeocacheIdModel
    {
        public int Id { get; set; }
        public int? GeocacheId { get; set; }
    }
}
