using System;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemModel
    {
        int Id { get; set; }
        string Name { get; set; }
        int? GeocacheId { get; set; }
        DateTime ActiveStartDate { get; set; }
        DateTime ActiveEndDate { get; set; }
    }
}