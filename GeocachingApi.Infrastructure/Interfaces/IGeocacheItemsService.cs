using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemsService
    {
        Task<IList<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly);
    }
}