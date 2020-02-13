namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemPatchGeocacheIdModel
    {
        int Id { get; set; }
        int? GeocacheId { get; set; }
    }
}