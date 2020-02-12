using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemsService
    {
        Task<IList<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly);
        Task<IGeocacheItem> CreateGeocacheItem(IGeocacheItem geocacheItem);
        IList<string> ValidateGeocacheItem(IGeocacheItem geocacheItem);
    }
}