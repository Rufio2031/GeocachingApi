

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheLocationModel
    {
        int Id { get; set; }
        decimal Longitude { get; set; }
        decimal Latitude { get; set; }
    }
}