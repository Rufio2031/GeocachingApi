using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<Geocache>> GetActiveGeocaches();
        Task<Geocache> GetGeocacheById(int id);
    }
}