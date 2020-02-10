using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemService
    {
        Task<IList<GeocacheItem>> GetActiveGeocacheItemsByGeocacheId(int id);
    }
}