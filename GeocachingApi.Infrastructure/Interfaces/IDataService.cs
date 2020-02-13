using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<GeocacheModel>> GetGeocaches();
        Task<GeocacheModel> GetGeocache(int id);
        Task<bool> GeocacheIdExists(int geocacheId);
        Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly);
        Task<GeocacheItemModel> GetGeocacheItem(int id);
        Task<GeocacheItemModel> GetGeocacheItem(string name);
        Task<GeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem);
        Task<GeocacheItemModel> UpdateGeocacheItemGeocacheId(int id, int? geocacheId);
    }
}