using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<GeocacheModel>> GetActiveGeocaches();
        Task<GeocacheModel> GetGeocacheById(int id);
        Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly);
        Task<GeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem);
        Task<bool> HasUniqueName(string name);
        Task<bool> GeocacheIdExists(int geocacheId);
    }
}