using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemsService
    {
        Task<GeocacheItemModel> GetGeocacheItem(int id);
        Task<IList<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly);
        Task<IGeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem);
        Task<IGeocacheItemModel> UpdateGeocacheItemGeocacheId(int id, int? geocacheId);
        Task<IList<string>> ValidateGeocacheItem(IGeocacheItemModel geocacheItem);
        Task<IList<string>> ValidateForUpdateGeocacheId(int id, int? geocacheId);
    }
}