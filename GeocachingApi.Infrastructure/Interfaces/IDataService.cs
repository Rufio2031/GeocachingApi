using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<Geocache>> GetActiveGeocaches();
        Task<Geocache> GetGeocacheById(int id);
        Task<IEnumerable<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly);
        Task<GeocacheItem> CreateGeocacheItem(IGeocacheItem geocacheItem);
        bool HasUniqueName(string name);
    }
}