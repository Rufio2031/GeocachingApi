
namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheModel
    {
        int Id { get; set; }
        string Name { get; set; }
        IGeocacheLocationModel Location { get; set; }
    }
}