

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheLocation
    {
        int Id { get; set; }
        decimal Longitude { get; set; }
        decimal Latitude { get; set; }
    }
}