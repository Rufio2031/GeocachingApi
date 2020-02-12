using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemsService
    {
        Task<IList<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly);
        Task<IGeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem);
        Task<IList<string>> ValidateGeocacheItem(IGeocacheItemModel geocacheItem);
        Task<IGeocacheItemModel> UpdateGeocacheItem(IGeocacheItemModel geocacheItem);
    }
}