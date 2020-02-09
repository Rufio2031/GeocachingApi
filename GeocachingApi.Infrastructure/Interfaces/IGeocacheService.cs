using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheService
    {
        Task<IList<Geocache>> GetActiveGeocaches();
        Task<Geocache> GetGeocacheById(int id);
    }
}